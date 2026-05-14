// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Microsoft.AspNetCore.Components;

namespace WebUI.Client.Components;

public partial class NavBar
{
    #region Injected Services
    [Inject]
    private NavigationManager Navigation { get; set; } = default!;
    [Inject]
    private WebUI.Client.AuthService AuthService { get; set; } = default!;
    #endregion
    private async Task Logout()
    {
        await AuthService.LogoutAsync();
        Navigation.NavigateTo("/");
    }
}