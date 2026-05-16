// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Sar;
using Core.Interfaces.Services;

using Microsoft.AspNetCore.Components;

namespace WebUI.Client.Components.Pages.Gdpr;

/// <summary>
/// Subject Access Request page — Admin generates GDPR Article 15 data exports (UC-010).
/// </summary>
public partial class SubjectAccessRequestPage : ComponentBase
{
    #region Injected Services

    [Inject]
    private ISubjectAccessRequestService SubjectAccessRequestService { get; set; } = default!;

    #endregion

    #region Fields

    private readonly SarExportRequestDto _request = new();
    private string _residentIdText = string.Empty;
    private bool _scopeNotes = true;
    private bool _scopeMedicine = true;
    private bool _scopePainkiller = true;
    private bool _scopeAudit = true;
    private bool _isGenerating;
    private bool _isFulfilling;
    private SarExportPackageDto? _generatedPackage;
    private string _feedbackMessage = string.Empty;
    private string _feedbackClass = string.Empty;

    #endregion

    #region Methods

    private async Task GenerateAsync()
    {
        ClearFeedback();

        if (!Guid.TryParse(_residentIdText, out Guid residentId) || residentId == Guid.Empty)
        {
            ShowFeedback("Please enter a valid resident GUID.", isError: true);
            return;
        }

        List<string> scopes = [];
        if (_scopeNotes) scopes.Add("Notes");
        if (_scopeMedicine) scopes.Add("Medicine");
        if (_scopePainkiller) scopes.Add("Painkiller");
        if (_scopeAudit) scopes.Add("Audit");

        if (scopes.Count == 0)
        {
            ShowFeedback("Select at least one data scope.", isError: true);
            return;
        }

        _isGenerating = true;
        try
        {
            SarExportRequestDto dto = new()
            {
                ResidentId = residentId,
                ScopeOptions = [.. scopes]
            };
            _generatedPackage = await SubjectAccessRequestService.GenerateExportAsync(dto, CancellationToken.None);
            ShowFeedback("Export package generated successfully.", isError: false);
        }
        catch (Exception ex)
        {
            ShowFeedback($"Failed to generate export: {ex.Message}", isError: true);
            _generatedPackage = null;
        }
        finally
        {
            _isGenerating = false;
        }
    }

    private async Task MarkFulfilledAsync()
    {
        if (_generatedPackage is null)
        {
            return;
        }

        _isFulfilling = true;
        try
        {
            SarFulfilledDto dto = new()
            {
                SarId = _generatedPackage.ExportId,
                FulfilledAt = DateTime.UtcNow
            };
            bool success = await SubjectAccessRequestService.MarkFulfilledAsync(dto, CancellationToken.None);
            if (success)
            {
                ShowFeedback("Subject Access Request marked as fulfilled.", isError: false);
                _generatedPackage = null;
                _residentIdText = string.Empty;
            }
            else
            {
                ShowFeedback("Failed to mark as fulfilled.", isError: true);
            }
        }
        catch (Exception ex)
        {
            ShowFeedback($"Failed to mark fulfilled: {ex.Message}", isError: true);
        }
        finally
        {
            _isFulfilling = false;
        }
    }

    private void ShowFeedback(string message, bool isError)
    {
        _feedbackMessage = message;
        _feedbackClass = isError ? "alert-danger" : "alert-success";
    }

    private void ClearFeedback()
    {
        _feedbackMessage = string.Empty;
        _feedbackClass = string.Empty;
    }

    #endregion
}
