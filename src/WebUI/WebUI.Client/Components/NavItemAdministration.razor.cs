// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebUI.Client.Components;

public partial class NavItemAdministration
{
    #region Injected Services
    [Inject]
    private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    #endregion

    #region Fields
    //private bool _isLoading = true;
    //private readonly string _errorMsg = string.Empty;
    #endregion

    #region Lifecycle
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _ = await AuthStateProvider.GetAuthenticationStateAsync();
        //_isLoading = false;
        StateHasChanged();
    }
    #endregion
}
