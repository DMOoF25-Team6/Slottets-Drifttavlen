// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.Providers;

public class DatabaseConnectionStateProvider
{
    public event Action? StateChanged;

    // Do not use autoproperty to avoid unnecessary event invocations
    private bool _isConnected;

    public bool IsConnected
    {
        get
        {
            return StateChanged == null || _isConnected;
        }
        private set
        {
            if (_isConnected != value)
            {
                _isConnected = value;
                StateChanged?.Invoke();
            }
        }
    }

    public void SetConnectionState(bool isConnected)
    {
        IsConnected = isConnected;
    }

    // Optionally, add async polling or update logic here
    public async Task PollConnectionStateAsync(Func<Task<bool>> checkConnection, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            bool state = await checkConnection();
            SetConnectionState(state);
            await Task.Delay(TimeSpan.FromSeconds(10), token);
        }
    }
}
