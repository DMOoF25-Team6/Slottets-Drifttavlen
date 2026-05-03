using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Providers;
using Xunit;

namespace Core.Tests.Providers
{
    public class DatabaseConnectionStateProviderTests
    {
        [Fact]
        public void SetConnectionState_UpdatesIsConnectedAndFiresEvent()
        {
            var provider = new DatabaseConnectionStateProvider();
            bool eventFired = false;
            provider.StateChanged += () => eventFired = true;

            provider.SetConnectionState(true);

            Assert.True(provider.IsConnected);
            Assert.True(eventFired);
        }

        [Fact]
        public void SetConnectionState_DoesNotFireEvent_WhenStateUnchanged()
        {
            var provider = new DatabaseConnectionStateProvider();
            provider.SetConnectionState(false); // initial state
            bool eventFired = false;
            provider.StateChanged += () => eventFired = true;

            provider.SetConnectionState(false);

            Assert.False(eventFired);
        }

        [Fact]
        public async Task PollConnectionStateAsync_UpdatesStateAndFiresEvent()
        {
            var provider = new DatabaseConnectionStateProvider();
            bool eventFired = false;
            provider.StateChanged += () => eventFired = true;
            var cts = new CancellationTokenSource();

            // Simulate connection check that returns true
            Task<bool> CheckConnection() => Task.FromResult(true);

            var pollTask = provider.PollConnectionStateAsync(CheckConnection, cts.Token);
            await Task.Delay(100, TestContext.Current.CancellationToken); // Give it time to run at least once
            cts.Cancel();
            try { await pollTask; } catch (OperationCanceledException) { }

            Assert.True(provider.IsConnected);
            Assert.True(eventFired);
        }
    }
}
