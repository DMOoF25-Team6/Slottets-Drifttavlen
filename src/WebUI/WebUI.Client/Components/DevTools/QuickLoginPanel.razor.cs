// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Microsoft.AspNetCore.Components;

using WebUI.Client.Components.Pages;

namespace WebUI.Client.Components.DevTools;

/// <summary>
/// Debug-only panel that provides one-click pre-filling of the login form
/// with seeded employee accounts. Only rendered in DEBUG builds.
/// </summary>
public partial class QuickLoginPanel : ComponentBase
{
    /// <summary>The parent login page, used to update credentials and trigger a re-render.</summary>
    [Parameter]
    public Login LoginPage { get; set; } = default!;

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
        new("Peder Rasmussen",  "PederRasmussen@example.com",  "admin",     ["(ingen)"],                             "Slottet"),
        new("Sanne Johansen",   "SanneJohansen@example.com",   "superuser", ["CanManageResidents","CanViewMedicine"], "Slottet"),
        new("Thor Danrsøn",     "ThorDanrsøn@example.com",     "user",      ["CanViewMedicine"],                     "Slottet"),
        new("Per Nielsen",      "PerNielsen@example.com",      "user",      ["CanViewMedicine"],                     "Skoven"),
        new("Anders Jensen",    "AndersJensen@example.com",    "user",      ["CanViewMedicine"],                     "Skoven"),
        new("Kasper Holm",      "KasperHolm@example.com",      "(ingen)",   ["(ingen)"],                             "Marken"),
    ];

    /// <summary>Pre-fills the login form with the selected employee's credentials.</summary>
    private async Task AutoFillAsync(DebugEmployee emp)
    {
        await LoginPage.SetQuickLoginCredentialsAsync(emp.Email, "Password123!");
    }
}
