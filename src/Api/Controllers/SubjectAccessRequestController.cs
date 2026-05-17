// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Text.Json;

using Core.DTOs.Sar;
using Core.Interfaces.Repositories;
using Core.Mappers;

using Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// API controller for handling GDPR Subject Access Requests (UC-010, Art. 15).
/// </summary>
/// <remarks>
/// Restricted to Admin role. Generates an export package containing all personal data
/// for a given resident across the scopes specified in the request.
/// </remarks>
[ApiController]
[Authorize(Roles = "admin")]
[Route("subjectaccessrequest")]
public class SubjectAccessRequestController : ControllerBase
{
    #region Fields

    private readonly IResidentRepository _residentRepository;
    private readonly IResidentNoteRepository _residentNoteRepository;
    private readonly IMedicineRepository _medicineRepository;
    private readonly IPainkillerRepository _painkillerRepository;
    private readonly IAuditRepository _auditRepository;
    private readonly IRetentionPolicyRepository _retentionPolicyRepository;

    #endregion

    #region Constructors

    public SubjectAccessRequestController(
        IResidentRepository residentRepository,
        IResidentNoteRepository residentNoteRepository,
        IMedicineRepository medicineRepository,
        IPainkillerRepository painkillerRepository,
        IAuditRepository auditRepository,
        IRetentionPolicyRepository retentionPolicyRepository)
    {
        ArgumentNullException.ThrowIfNull(residentRepository);
        ArgumentNullException.ThrowIfNull(residentNoteRepository);
        ArgumentNullException.ThrowIfNull(medicineRepository);
        ArgumentNullException.ThrowIfNull(painkillerRepository);
        ArgumentNullException.ThrowIfNull(auditRepository);
        ArgumentNullException.ThrowIfNull(retentionPolicyRepository);
        _residentRepository = residentRepository;
        _residentNoteRepository = residentNoteRepository;
        _medicineRepository = medicineRepository;
        _painkillerRepository = painkillerRepository;
        _auditRepository = auditRepository;
        _retentionPolicyRepository = retentionPolicyRepository;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Generates a GDPR Article 15 Subject Access Request export package for a resident.
    /// </summary>
    /// <param name="dto">The SAR request including resident id and data scopes to include.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The generated export package metadata (file name, generation timestamp, export id).</returns>
    /// <remarks>POST /subjectaccessrequest/export</remarks>
    [HttpPost("export")]
    public async Task<ActionResult<SarExportPackageDto>> GenerateExport(
        [FromBody] SarExportRequestDto dto,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (dto.ResidentId == Guid.Empty)
        {
            return BadRequest("ResidentId is required.");
        }

        Resident? resident = await _residentRepository.GetByIdAsync(dto.ResidentId, cancellationToken);
        if (resident is null)
        {
            return NotFound($"No resident found with id {dto.ResidentId}.");
        }

        Guid exportId = Guid.NewGuid();
        DateTime generatedAt = DateTime.UtcNow;
        string fileName = $"sar-export-{resident.Initials}-{generatedAt:yyyyMMddHHmmss}.json";

        // Build a GDPR Art. 15(1)(a)-(h) compliant export. Scope selection follows the
        // operation contract in uc-010.oc.da.md: only the categories the Admin selected
        // in ScopeOptions are populated, satisfying the data minimisation principle of
        // GDPR Art. 5(1)(c) on the export itself.
        HashSet<string> scopes = new(dto.ScopeOptions ?? [], StringComparer.OrdinalIgnoreCase);

        IEnumerable<ResidentNote> notes = scopes.Contains("Notes")
            ? (await _residentNoteRepository.GetAllAsync(cancellationToken)).Where(n => n.ResidentId == resident.Id)
            : [];
        IEnumerable<MedicineRecord> medicines = scopes.Contains("Medicine")
            ? (await _medicineRepository.GetAllAsync(cancellationToken)).Where(m => m.ResidentId == resident.Id)
            : [];
        IEnumerable<PainkillerRecord> painkillers = scopes.Contains("Painkiller")
            ? (await _painkillerRepository.GetAllAsync(cancellationToken)).Where(p => p.ResidentId == resident.Id)
            : [];

        // EU Court of Justice (case C-579/21, 22 June 2023): log file entries about the
        // data subject are part of the Art. 15 right of access. We include audit entries
        // referencing this resident so the export reflects who accessed which data when.
        IEnumerable<AuditEntry> auditEntries = scopes.Contains("Audit")
            ? (await _auditRepository.GetAllAsync(cancellationToken))
                .Where(a => a.Metadata.Contains(resident.Id.ToString(), StringComparison.OrdinalIgnoreCase))
            : [];

        IEnumerable<RetentionPolicy> retentionPolicies =
            await _retentionPolicyRepository.GetAllAsync(cancellationToken);

        var artifact = new
        {
            exportId,
            generatedAt,
            // Art. 15(1) header block
            controller = new
            {
                name = "Slottet Care Home",
                contact = "dpo@slottet.example",   // configurable per deployment
                legalBasis = "GDPR Art. 6(1)(c)+(e), Art. 9(2)(h); Sundhedsloven; Autorisationsloven §22"
            },
            subject = new
            {
                residentId = resident.Id,
                initials = resident.Initials,
                firstName = resident.FirstName,
                lastName = resident.LastName,
                department = resident.Department.ToString()
            },
            // Art. 15(1)(a) purposes
            purposes = new[]
            {
                "Care administration and resident overview",
                "Medical journal maintenance (Autorisationsloven §22)",
                "Incident detection and audit trail (GDPR Art. 32)"
            },
            // Art. 15(1)(b) categories of personal data
            categories = new
            {
                residentNotes = notes.Select(n => new { n.Id, n.CreatedAt, n.EditedAt, n.Note }),
                medicineLogs = medicines.Select(m => new { m.Id, m.MedicineName, m.Timestamp, m.Given }),
                painkillerLogs = painkillers.Select(p => new { p.Id, p.Type, p.GivenAt, p.NextAllowedTime }),
                auditEntries = auditEntries.Select(a => new { a.Id, a.Metadata, a.StartTimeUtc, a.EndTimeUtc, a.Succeeded, a.UserId })
            },
            // Art. 15(1)(c) recipients
            recipients = new[] { "Internal: Care staff (department-scoped)", "Internal: System administrators (audit only)" },
            // Art. 15(1)(d) retention periods (current effective policies)
            retentionPolicies = retentionPolicies.Select(p => new
            {
                category = p.Category.ToString(),
                periodDays = (int)p.RetentionPeriod.TotalDays,
                legalMinimumDays = (int)p.LegalMinimum.TotalDays,
                p.EffectiveFrom
            }),
            // Art. 15(1)(g) source of data
            dataSource = "Collected directly from the resident and from care staff during care provision.",
            // Art. 15(1)(e)+(f) rights notice
            rights = new[]
            {
                "Right to rectification (GDPR Art. 16)",
                "Right to erasure (GDPR Art. 17, subject to Autorisationsloven §22 retention)",
                "Right to restriction (GDPR Art. 18)",
                "Right to lodge a complaint with Datatilsynet (GDPR Art. 77)"
            },
            // Art. 15(1)(h) transfers
            transferredToThirdCountry = false,
            // Art. 22 automated decision-making
            automatedDecisionMaking = false
        };

        string payload = JsonSerializer.Serialize(artifact, new JsonSerializerOptions
        {
            WriteIndented = true,
            // Camel-case for downstream tooling compatibility.
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        SarExportPackageDto package = SarExportMapper.ToPackageDto(exportId, generatedAt, fileName, payload);
        return Ok(package);
    }

    /// <summary>
    /// Marks a Subject Access Request as fulfilled (delivered to the data subject).
    /// </summary>
    /// <param name="dto">The fulfillment payload with SAR id and timestamp.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the SAR was marked fulfilled.</returns>
    /// <remarks>POST /subjectaccessrequest/fulfilled</remarks>
    [HttpPost("fulfilled")]
    public Task<ActionResult<bool>> MarkFulfilled(
        [FromBody] SarFulfilledDto dto,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (dto.SarId == Guid.Empty)
        {
            return Task.FromResult<ActionResult<bool>>(BadRequest("SarId is required."));
        }

        // SAR fulfilment is currently recorded via AuditInterceptor (UC-009): the export
        // generation and this fulfilment confirmation each produce an AuditEntry that
        // preserves the chronology required by GDPR Art. 12(3) ("provide information on
        // action taken... without undue delay and at the latest within one month").
        //
        // A dedicated SubjectAccessRequest entity (with FulfilledAt, FulfilledByEmployeeId,
        // ExportFileName) is intentionally deferred until a schema migration is scheduled.
        // The migration adds a new table and so must be coordinated with the database
        // operator; see /docs/use-cases/uc-010-ensure-data-security-and-gdpr-compliance/.
        return Task.FromResult<ActionResult<bool>>(Ok(true));
    }

    #endregion
}
