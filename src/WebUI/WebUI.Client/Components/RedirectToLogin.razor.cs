// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Microsoft.AspNetCore.Components;

namespace WebUI.Client.Components;

public partial class RedirectToLogin : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private bool hasRedirected;

    /// <summary>
    /// Redirects unauthorized users to the login page, unless already on the login page.
    /// Uses OnAfterRenderAsync to avoid NavigationException during render.
    /// </summary>
    /// <param name="firstRender">Indicates if this is the first render.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !hasRedirected)
        {
            string uri = NavigationManager.ToBaseRelativePath(NavigationManager.Uri).TrimStart('/');
            // Check for login page with or without query parameters
            if (!uri.StartsWith("login", StringComparison.OrdinalIgnoreCase))
            {
                hasRedirected = true;
                NavigationManager.NavigateTo($"login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}", forceLoad: true);
            }
        }
        return Task.CompletedTask;
    }
}
