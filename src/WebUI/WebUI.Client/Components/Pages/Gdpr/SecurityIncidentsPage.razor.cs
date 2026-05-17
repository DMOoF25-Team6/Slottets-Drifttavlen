// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Security;
using Core.Interfaces.Services;

using Domain.Enums;

using Microsoft.AspNetCore.Components;

namespace WebUI.Client.Components.Pages.Gdpr;

/// <summary>
/// Security Incidents page — Admin investigates, escalates, and closes security incidents (UC-010).
/// </summary>
/// <remarks>
/// Escalation with breach=true triggers GDPR Article 33 notification to the DPO via Art33NotificationService.
/// </remarks>
public partial class SecurityIncidentsPage : ComponentBase
{
    #region Injected Services

    [Inject]
    private ISecurityIncidentService SecurityIncidentService { get; set; } = default!;

    #endregion

    #region Fields

    private List<SecurityIncidentDto> _incidents = [];
    private SecurityIncidentDto? _selectedIncident;
    private bool _isLoading = true;
    private bool _confirmingBreach;
    private string _newNote = string.Empty;
    private string _feedbackMessage = string.Empty;
    private string _feedbackClass = string.Empty;

    #endregion

    #region Lifecycle

    protected override async Task OnInitializedAsync()
    {
        await LoadIncidentsAsync();
    }

    #endregion

    #region Methods

    private async Task LoadIncidentsAsync()
    {
        _isLoading = true;
        try
        {
            IEnumerable<SecurityIncidentDto> result = await SecurityIncidentService.GetIncidentsAsync(CancellationToken.None);
            _incidents = [.. result];
            if (_selectedIncident is not null)
            {
                _selectedIncident = _incidents.FirstOrDefault(i => i.Id == _selectedIncident.Id);
            }
        }
        catch (Exception ex)
        {
            ShowFeedback($"Failed to load incidents: {ex.Message}", isError: true);
        }
        finally
        {
            _isLoading = false;
        }
    }

    private void SelectIncident(SecurityIncidentDto incident)
    {
        _selectedIncident = incident;
        _confirmingBreach = false;
        _newNote = string.Empty;
        ClearFeedback();
    }

    private void StartBreachConfirm()
    {
        _confirmingBreach = true;
        ClearFeedback();
    }

    private async Task EscalateAsync(bool isBreach)
    {
        if (_selectedIncident is null)
        {
            return;
        }

        try
        {
            SecurityIncidentDto updated = await SecurityIncidentService.EscalateIncidentAsync(
                _selectedIncident.Id,
                isBreach,
                CancellationToken.None);

            string action = isBreach ? "GDPR Art. 33 breach notification sent to DPO" : "Incident escalated";
            ShowFeedback(action, isError: false);
            _confirmingBreach = false;
            _selectedIncident = updated;
            await LoadIncidentsAsync();
        }
        catch (Exception ex)
        {
            ShowFeedback($"Failed to escalate: {ex.Message}", isError: true);
        }
    }

    private async Task AddNoteAsync()
    {
        if (_selectedIncident is null || string.IsNullOrWhiteSpace(_newNote))
        {
            return;
        }

        try
        {
            AddInvestigationNotesDto dto = new()
            {
                IncidentId = _selectedIncident.Id,
                Notes = _newNote
            };

            SecurityIncidentDto updated = await SecurityIncidentService.AddInvestigationNotesAsync(dto, CancellationToken.None);
            ShowFeedback("Investigation note added.", isError: false);
            _newNote = string.Empty;
            _selectedIncident = updated;
            await LoadIncidentsAsync();
        }
        catch (Exception ex)
        {
            ShowFeedback($"Failed to add note: {ex.Message}", isError: true);
        }
    }

    private async Task CloseIncidentAsync()
    {
        if (_selectedIncident is null)
        {
            return;
        }

        try
        {
            SecurityIncidentDto updated = await SecurityIncidentService.CloseIncidentAsync(_selectedIncident.Id, CancellationToken.None);
            ShowFeedback("Incident closed.", isError: false);
            _selectedIncident = updated;
            await LoadIncidentsAsync();
        }
        catch (Exception ex)
        {
            ShowFeedback($"Failed to close incident: {ex.Message}", isError: true);
        }
    }

    private static string GetSeverityBadgeClass(IncidentSeverity severity) => severity switch
    {
        IncidentSeverity.Critical => "bg-danger",
        IncidentSeverity.High => "bg-warning text-dark",
        IncidentSeverity.Medium => "bg-info text-dark",
        IncidentSeverity.Low => "bg-secondary",
        _ => "bg-light text-dark"
    };

    private static string GetStatusBadgeClass(IncidentStatus status) => status switch
    {
        IncidentStatus.Open => "bg-danger",
        IncidentStatus.UnderInvestigation => "bg-warning text-dark",
        IncidentStatus.Escalated => "bg-warning text-dark",
        IncidentStatus.BreachNotified => "bg-dark",
        IncidentStatus.Closed => "bg-success",
        _ => "bg-light text-dark"
    };

    /// <summary>
    /// Returns the Bootstrap badge class for the Art. 33 notification deadline countdown.
    /// Once the incident has been notified or closed, the deadline is informational only.
    /// </summary>
    private static string GetDeadlineBadgeClass(TimeSpan remaining, IncidentStatus status)
    {
        if (status == IncidentStatus.BreachNotified || status == IncidentStatus.Closed)
        {
            return "bg-secondary";
        }
        if (remaining <= TimeSpan.Zero)
        {
            return "bg-danger";
        }
        if (remaining <= TimeSpan.FromHours(24))
        {
            return "bg-warning text-dark";
        }
        return "bg-info text-dark";
    }

    /// <summary>
    /// Human-readable countdown to the GDPR Art. 33(1) 72-hour deadline.
    /// </summary>
    private static string FormatDeadline(TimeSpan remaining, IncidentStatus status)
    {
        if (status == IncidentStatus.BreachNotified)
        {
            return "Notified";
        }
        if (status == IncidentStatus.Closed)
        {
            return "Closed (no notification)";
        }
        if (remaining <= TimeSpan.Zero)
        {
            return $"OVERDUE by {Math.Abs(remaining.TotalHours):N0}h";
        }
        return remaining.TotalHours >= 1
            ? $"{remaining.TotalHours:N0}h remaining"
            : $"{remaining.TotalMinutes:N0}m remaining";
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
