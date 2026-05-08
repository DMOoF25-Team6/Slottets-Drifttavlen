// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebUI.Client.Components.Pages;

public partial class Home
{
    [Inject]
    private NavigationManager Navigation { get; set; } = default!;
    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    [Inject]
    private WebUI.Client.AuthService AuthService { get; set; } = default!;

    private bool isInteractive = false;
    private System.Security.Claims.ClaimsPrincipal? user;
    private string? currentUrl;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            isInteractive = true;
            currentUrl = Navigation.Uri;
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            user = authState.User;
            StateHasChanged();
        }
    }

    private void Login()
    {
        if (isInteractive)
        {
            Navigation.NavigateTo("/login");
        }
    }

    private async Task Logout()
    {
        await AuthService.LogoutAsync();
        Navigation.NavigateTo("/");
    }

}
