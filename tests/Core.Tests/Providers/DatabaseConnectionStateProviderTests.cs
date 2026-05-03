// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Providers;

namespace Core.Tests.Providers;

public class DatabaseConnectionStateProviderTests
{
    [Fact]
    public void SetConnectionState_UpdatesIsConnectedAndFiresEvent()
    {
        DatabaseConnectionStateProvider provider = new();
        bool eventFired = false;
        provider.StateChanged += () => eventFired = true;

        provider.SetConnectionState(true);

        Assert.True(provider.IsConnected);
        Assert.True(eventFired);
    }

    [Fact]
    public void SetConnectionState_DoesNotFireEvent_WhenStateUnchanged()
    {
        DatabaseConnectionStateProvider provider = new();
        provider.SetConnectionState(false); // initial state
        bool eventFired = false;
        provider.StateChanged += () => eventFired = true;

        provider.SetConnectionState(false);

        Assert.False(eventFired);
    }

    [Fact]
    public async Task PollConnectionStateAsync_UpdatesStateAndFiresEvent()
    {
        DatabaseConnectionStateProvider provider = new();
        bool eventFired = false;
        provider.StateChanged += () => eventFired = true;
        CancellationTokenSource cts = new();

        // Simulate connection check that returns true
        static Task<bool> CheckConnection() => Task.FromResult(true);

        Task pollTask = provider.PollConnectionStateAsync(CheckConnection, cts.Token);
        await Task.Delay(100, TestContext.Current.CancellationToken); // Give it time to run at least once
        cts.Cancel();
        try { await pollTask; } catch (OperationCanceledException) { }

        Assert.True(provider.IsConnected);
        Assert.True(eventFired);
    }
}
