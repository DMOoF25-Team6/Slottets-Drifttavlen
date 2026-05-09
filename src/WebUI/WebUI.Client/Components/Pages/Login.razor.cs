// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebUI.Client.Components.Pages;

public partial class Login
{
    // Indicates if the app is running in DEBUG mode for conditional UI rendering
    public bool IsDebug { get; private set; }

    public Login()
    {
        IsDebug = false; // Default value
#if DEBUG
        IsDebug = true;
#endif
    }
    [Inject]
    private AuthService AuthService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;


    private readonly LoginModel loginModel = new();
    private string? errorMessage;


#if DEBUG

    // ── Debug helpers ────────────────────────────────────────────────────────
    /// <summary>Lightweight view-model used only in the debug quick-login panel.</summary>
    private sealed record DebugEmployee(
        string Name,
        string Email,
        string Role,
        IReadOnlyList<string> Claims,
        string? Department);

    /// <summary>
    /// Static list that mirrors <c>IdentitySeed</c> and <c>EmployeeConfiguration</c>.
    /// All users share the development password <c>Password123!</c>.
    /// </summary>
    private static readonly IReadOnlyList<DebugEmployee> DebugEmployees =
    [
        new("Peder Rasmussen",  "PederRasmussen@example.com",  "admin",     ["(ingen)"],                            "Slottet"),
        new("Sanne Johansen",   "SanneJohansen@example.com",   "superuser", ["CanManageResidents","CanViewMedicine"],"Slottet"),
        new("Thor Danrsøn",     "ThorDanrsøn@example.com",     "user",      ["CanViewMedicine"],                    "Slottet"),
        new("Per Nielsen",      "PerNielsen@example.com",      "user",      ["CanViewMedicine"],                    "Skoven"),
        new("Anders Jensen",    "AndersJensen@example.com",    "user",      ["CanViewMedicine"],                    "Skoven"),
        new("Kasper Holm",      "KasperHolm@example.com",      "(ingen)",   ["(ingen)"],                            "Marken"),
    ];

    private void AutoFill(DebugEmployee emp)
    {
        loginModel.Username = emp.Email;
        loginModel.Password = "Password123!";
    }
#endif
    [Parameter]
    [SupplyParameterFromQuery(Name = "returnUrl")]
    public string? ReturnUrl { get; set; }


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



    public class LoginModel
    {
        [Required(ErrorMessage = "Brugernavn er påkrævet.")]
        [EmailAddress(ErrorMessage = "Brugernavn skal være en gyldig e-mailadresse.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Adgangskode er påkrævet.")]
        public string? Password { get; set; }
    }
}
