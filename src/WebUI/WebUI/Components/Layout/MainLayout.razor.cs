// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Providers;
using Core.Interfaces.Services;

using Microsoft.AspNetCore.Components;

namespace WebUI.Components.Layout;

public partial class MainLayout : IDisposable
{

    [Inject]
    private IDatabaseConnectionStateProvider DbConnectionStateProvider { get; set; } = default!;

    [Inject]
    private IDatabaseConnectionService DbConnectionService { get; set; } = default!;

    private System.Threading.Timer? _timer;

    // Dotnet 8 issue
#pragma warning disable IDE0032 // Use auto property
    private bool _isDbConnected;
#pragma warning restore IDE0032 // Use auto property

    private bool IsDbConnected
    {
        get => _isDbConnected;
        set
        {
            if (_isDbConnected != value)
            {
                _isDbConnected = value;
                StateHasChanged();
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        DbConnectionStateProvider.StateChanged += OnDbConnectionStateChanged;
        await FetchDbConnectionStateAsync();
        // Poll every 30 seconds (adjust as needed)
        _timer = new System.Threading.Timer(async _ =>
        {
            await InvokeAsync(FetchDbConnectionStateAsync);
        }, null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));
    }

    private async Task FetchDbConnectionStateAsync()
    {
        await DbConnectionService.CheckDatabaseConnectionAsync();
        IsDbConnected = DbConnectionStateProvider.IsConnected;
    }

    private void OnDbConnectionStateChanged()
    {
        IsDbConnected = DbConnectionStateProvider.IsConnected;
    }

    void IDisposable.Dispose()
    {
        DbConnectionStateProvider.StateChanged -= OnDbConnectionStateChanged;
        _timer?.Dispose();
        // Suppress finalization to avoid unnecessary GC overhead
        GC.SuppressFinalize(this);
    }
}
