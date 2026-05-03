// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Managers;
using Core.Providers;

using Microsoft.AspNetCore.Components;

namespace WebUI.Components.Layout;

public partial class MainLayout
{
    private System.Threading.Timer? _timer;

    [Inject]
    private IDatabaseConnectionManager DbConnectionManager { get; set; } = default!;

    [Inject]
    private DatabaseConnectionStateProvider DbStateProvider { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        DbStateProvider.StateChanged += OnDbStateChanged;
        await DbConnectionManager.CheckAndUpdateConnectionStateAsync();
        // Poll every 30 seconds (adjust as needed)
        _timer = new System.Threading.Timer(async _ =>
        {
            await InvokeAsync(() => DbConnectionManager.CheckAndUpdateConnectionStateAsync());
        }, null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));
    }

    private void OnDbStateChanged()
    {
        _ = InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        DbStateProvider.StateChanged -= OnDbStateChanged;
        _timer?.Dispose();
    }
}
