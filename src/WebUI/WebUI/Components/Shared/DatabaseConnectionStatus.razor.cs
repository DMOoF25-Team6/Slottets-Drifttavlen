// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Providers;

using Microsoft.AspNetCore.Components;

namespace WebUI.Components.Shared;

/// <summary>
/// A component that displays the status of the database connection.
/// It listens to changes in the database connection state and updates the UI accordingly.
/// The component implements IDisposable to unsubscribe from events when it is disposed, preventing memory leaks. 
/// </summary>
/// <remarks>
/// The component subscribes to the StateChanged event of the database connection state provider in the OnInitialized method.
/// </remarks>
public partial class DatabaseConnectionStatus : IDisposable
{
    /// <summary>
    /// The provider that exposes the current database connection state.
    /// Injected from DI.
    /// </summary>
    [Inject]
    public IDatabaseConnectionStateProvider DbStateProvider { get; set; } = default!;

    /// <summary>
    /// Local copy of the connection state used for rendering.
    /// </summary>
    protected bool IsConnected { get; private set; }

    protected override void OnInitialized()
    {
        // Read initial state so the component renders correctly on first render.
        IsConnected = DbStateProvider.IsConnected;

        // Subscribe to subsequent state changes.
        DbStateProvider.StateChanged += OnStateChanged;
    }

    // This method is called whenever the database connection state changes.
    private void OnStateChanged()
    {
        // Update local state from provider and request a UI refresh on the correct context.
        _ = InvokeAsync(() =>
        {
            IsConnected = DbStateProvider.IsConnected;
            StateHasChanged(); // SignalR over a persistent WebSocket connection to send DOM updates to the browser.
        });
    }

    public void Dispose()
    {
        // Unsubscribe to avoid memory leaks. Guard against null/partial construction.
        DbStateProvider?.StateChanged -= OnStateChanged;

        GC.SuppressFinalize(this);
    }
}