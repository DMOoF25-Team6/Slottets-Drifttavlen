// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebUI.Client.Components.Pages;

public partial class Dashboard
{
    // UC-001: Credentials for the read-only kiosk account used by unattended dashboard screens.
    private const string DashboardEmail = "dashboard@slottet.dk";
    private const string DashboardPassword = "Dashboard123!";

    //private static readonly string DashboardEmail = Environment.GetEnvironmentVariable("Dashboard__AutoLoginEmail") ?? string.Empty;
    //private static readonly string DashboardPassword = Environment.GetEnvironmentVariable("Dashboard__AutoLoginPassword") ?? string.Empty;

    [Inject]
    private AuthService AuthService { get; set; } = default!;

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    private IEnumerable<Resident> _residents = [];
    private bool _isLoading = true;

    //[Parameter]
    //public int? DepartmentId { get; set; }

    // JS interop (localStorage) is only available after the component is rendered interactively
    // on the client, so login and data loading must happen in OnAfterRenderAsync.
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        // Auto-login as the read-only dashboard kiosk user if not already authenticated.
        AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        if (authState.User.Identity is not { IsAuthenticated: true })
        {
            _ = await AuthService.LoginAsync(DashboardEmail, DashboardPassword);
        }

        IEnumerable<Resident> residents = await ResidentService.GetAllAsync();
        _residents = [.. residents.Select(dto => new Resident
        {
            Id = dto.Id,
            Initials = dto.Initials,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            TrafficLightStatus = dto.TrafficLightStatus.HasValue
                ? dto.TrafficLightStatus.Value
                : null,
            Notes = [.. dto.Notes.Select(n => new ResidentNote
            {
                Id = n.Id,
                Note = n.Note,
                EditedAt = n.EditedAt,
                ResidentId = dto.Id
            })]
        })];

        _isLoading = false;
        StateHasChanged();
    }
}
