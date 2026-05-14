// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Security.Claims;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebUI.Client.Components;

public partial class NavItemDashboard
{
    #region Injected Services
    [Inject]
    private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    #endregion

    #region Fields
    private readonly List<(string Label, string Url)> dashboardLinks = [];

    private bool _isLoading = true;
    private readonly string _errorMsg = string.Empty;
    #endregion

    #region Lifecycle
    //protected override async Task OnInitializedAsync()
    //{
    //}

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            _isLoading = false;
            return;
        }
        AuthenticationState authState = await AuthStateProvider.GetAuthenticationStateAsync();
        ClaimsPrincipal user = authState.User;
        if (user.Identity is not { IsAuthenticated: true })
            return;

        List<Claim> claims = user.Claims.ToList();
        foreach (Claim claim in claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
        }
        bool all = claims.Any(c => c.Type == "Permission" && c.Value.StartsWith("department:all:"));
        if (all)
        {
            dashboardLinks.Add(("Slottet", "/dashboard/slottet"));
            dashboardLinks.Add(("Marken", "/dashboard/marken"));
            dashboardLinks.Add(("Skoven", "/dashboard/skoven"));
        }
        else
        {
            if (claims.Any(c => c.Type == "Permission" && c.Value.StartsWith("department:slottet:")))
                dashboardLinks.Add(("Slottet", "/dashboard/slottet"));
            if (claims.Any(c => c.Type == "Permission" && c.Value.StartsWith("department:marken:")))
                dashboardLinks.Add(("Marken", "/dashboard/marken"));
            if (claims.Any(c => c.Type == "Permission" && c.Value.StartsWith("department:skoven:")))
                dashboardLinks.Add(("Skoven", "/dashboard/skoven"));
        }
        _isLoading = false;
        StateHasChanged();
    }

    #endregion

    #region Method Helpers

    #endregion
}
