// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace WebUI.Components.Shared;

public partial class DatabaseConnectionStatus : IDisposable
{
    protected override void OnInitialized()
    {
        DbStateProvider.StateChanged += OnStateChanged;
    }

    private void OnStateChanged()
    {
        _ = InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        DbStateProvider.StateChanged -= OnStateChanged;
        GC.SuppressFinalize(this);
    }
}