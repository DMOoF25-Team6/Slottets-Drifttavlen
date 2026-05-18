// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebUI.Client.Components.Pages.Gdpr;

/// <summary>
/// GDPR Compliance Dashboard — central hub for Admin to access all UC-010 features.
/// </summary>
public partial class GdprDashboardPage : ComponentBase
{
    #region Injected Services

    [Inject]
    private IAnonymizationService AnonymizationService { get; set; } = default!;

    [Inject]
    private ISecurityIncidentService SecurityIncidentService { get; set; } = default!;
    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    #endregion

    #region Fields

    private int _pendingCandidates;
    private int _openIncidents;
    private string? _loadError;

    #endregion

    #region Lifecycle

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }
        // Auto-login as the read-only dashboard kiosk user if not already authenticated.
        await InitializeAuthorizationAsync();
        await LoadCountersAsync();
        StateHasChanged();
    }

    private async Task InitializeAuthorizationAsync()
    {
        AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        _ = authState.User;
    }
    #endregion

    #region Methods

    private async Task LoadCountersAsync()
    {
        try
        {
            IEnumerable<Core.DTOs.Anonymization.AnonymizationCandidateDto> candidates =
                await AnonymizationService.GetCandidatesAsync(CancellationToken.None);
            _pendingCandidates = candidates.Count(c => c.Status == Domain.Enums.AnonymizationStatus.Pending);

            IEnumerable<Core.DTOs.Security.SecurityIncidentDto> incidents =
                await SecurityIncidentService.GetIncidentsAsync(CancellationToken.None);
            _openIncidents = incidents.Count(i =>
                i.Status is Domain.Enums.IncidentStatus.Open or
                Domain.Enums.IncidentStatus.UnderInvestigation);
        }
        catch (Exception ex)
        {
            _pendingCandidates = 0;
            _openIncidents = 0;
            _loadError = ex.Message;
        }
    }

    #endregion
}
