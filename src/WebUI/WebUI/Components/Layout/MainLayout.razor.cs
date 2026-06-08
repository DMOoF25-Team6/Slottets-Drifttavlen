// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Providers;
using Core.Interfaces.Services;

using Microsoft.AspNetCore.Components;

namespace WebUI.Components.Layout;

public partial class MainLayout
{

    [Inject]
    private IDatabaseConnectionStateProvider DbConnectionStateProvider { get; set; } = default!;

    [Inject]
    private IDatabaseConnectionService? DatabaseConnectionService { get; set; } = default!;

    protected override void OnInitialized()
    {
    }
}
