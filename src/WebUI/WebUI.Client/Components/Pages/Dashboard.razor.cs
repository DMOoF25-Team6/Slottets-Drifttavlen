// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebUI.Client.Components.Pages;

public partial class Dashboard
{
    #region Injected Services
    [Inject]
    private AuthService AuthService { get; set; } = default!;

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    #endregion
    #region Fields

    // UC-001: Credentials for the read-only kiosk account used by unattended dashboard screens.
    private const string DashboardEmail = "dashboard@slottet.dk";
    private const string DashboardPassword = "Dashboard123!";

    //private static readonly string DashboardEmail = Environment.GetEnvironmentVariable("Dashboard__AutoLoginEmail") ?? string.Empty;
    //private static readonly string DashboardPassword = Environment.GetEnvironmentVariable("Dashboard__AutoLoginPassword") ?? string.Empty;


    private IEnumerable<Resident> _residents = [];
    private bool _isLoading = true;

    [Parameter]
    public string? Department { get; set; }
    #endregion
    #region Lifecycle
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
            if (!await AuthService.LoginAsync(DashboardEmail, DashboardPassword))
            {
                throw new Exception("Failed to auto-login to dashboard account. Check credentials and ensure the account exists.");
            }
            // make sure that browser storage is updated with the new auth state before proceeding to load data
            _ = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        }
        IEnumerable<Resident> residents = [];

        Department parsedDepartment = default;

        // Validate Department parameter if provided, and set debug message
        if (Department is not null)
        {
            if (!Enum.TryParse<Department>(Department, true, out parsedDepartment))
            {
                Department = null;
            }
        }

        residents = Department == null ? await ResidentService.GetAllAsync() : await ResidentService.GetByDepartmentsAsync([parsedDepartment]);

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
    #endregion
}
