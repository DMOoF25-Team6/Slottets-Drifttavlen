// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebUI.Client.Components.Pages;

public partial class Login
{
    [Inject]
    private AuthService AuthService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    private readonly LoginModel loginModel = new();
    private string? errorMessage;

    // Indicates if the app is running in DEBUG mode for conditional UI rendering
    public bool IsDebug { get; private set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "returnUrl")]
    public string? ReturnUrl { get; set; }

    public Login()
    {
#if DEBUG
        IsDebug = true;
#else
        IsDebug = false;
#endif        
    }


    private async Task HandleLogin()
    {
        errorMessage = "";
        bool success = await AuthService.LoginAsync(loginModel.Username ?? string.Empty, loginModel.Password ?? string.Empty);
        if (!success)
        {
            errorMessage = "Ugyldigt brugernavn eller adgangskode.";
        }
        else
        {
            errorMessage = null;
            string redirectUrl = !string.IsNullOrWhiteSpace(ReturnUrl) ? ReturnUrl : "/";
            Navigation.NavigateTo(redirectUrl!);
        }
    }

    private async Task Logout()
    {
        await AuthService.LogoutAsync();
        Navigation.NavigateTo(Navigation.Uri, new NavigationOptions { ForceLoad = true }); // Refresh UI
    }

    /// <summary>
    /// Sets the login credentials from the debug quick-login panel and triggers a re-render
    /// on the correct Blazor renderer context.
    /// </summary>
    /// <param name="email">The email address to pre-fill.</param>
    /// <param name="password">The password to pre-fill.</param>
    /// <returns>A task that completes when the render request has been dispatched.</returns>
    public Task SetQuickLoginCredentialsAsync(string email, string password)
    {
        loginModel.Username = email;
        loginModel.Password = password;

        return InvokeAsync(StateHasChanged);
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Brugernavn er påkrævet.")]
        [EmailAddress(ErrorMessage = "Brugernavn skal være en gyldig e-mailadresse.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Adgangskode er påkrævet.")]
        public string? Password { get; set; }
    }
}
