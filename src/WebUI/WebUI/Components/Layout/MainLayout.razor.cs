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
    private IDatabaseConnectionService? DatabaseConnectionService { get; set; } = default!;

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

    protected override void OnInitialized()
    {
        DbConnectionStateProvider.StateChanged += OnDbConnectionStateChanged;
        _ = DatabaseConnectionService!.CheckDatabaseConnectionAsync();
        IsDbConnected = DbConnectionStateProvider.IsConnected;
    }

    private void OnDbConnectionStateChanged()
    {
        _ = InvokeAsync(() =>
        {
            IsDbConnected = DbConnectionStateProvider.IsConnected;
        });
    }

    void IDisposable.Dispose()
    {
        //DbConnectionStateProvider.StateChanged -= OnDbConnectionStateChanged;
        // Suppress finalization to avoid unnecessary GC overhead
        GC.SuppressFinalize(this);
    }
}
