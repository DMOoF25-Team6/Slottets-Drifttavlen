// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;

using Microsoft.AspNetCore.Components;

namespace WebUI.Components.PhoneList;

/// <summary>
/// Code-behind for the PhoneList dashboard component.
/// Displays fixed work phone assignments for the active shift,
/// and auto-refreshes every 60 seconds driven by a system time event.
/// </summary>
public partial class PhoneList : ComponentBase, IDisposable
{
    #region Injected Services

    [Inject]
    private IPhoneAssignmentManager PhoneAssignmentManager { get; set; } = default!;

    #endregion

    #region Fields

    private IEnumerable<PhoneAssignmentDto> _assignments = [];
    private string _activeShift = string.Empty;
    private DateTime _lastUpdated = DateTime.Now;
    private bool _isLoading = true;
    private bool _hasError;
    private Timer? _refreshTimer;

    #endregion

    #region Lifecycle

    protected override async Task OnInitializedAsync()
    {
        await LoadPhoneAssignments();

        // Auto-refresh every 60 seconds driven by system time event
        _refreshTimer = new Timer(async _ =>
        {
            await LoadPhoneAssignments();
            await InvokeAsync(StateHasChanged);
        }, null, TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(60));
    }

    #endregion

    #region Methods

    private async Task LoadPhoneAssignments()
    {
        _isLoading = true;
        _hasError = false;

        try
        {
            _assignments = await PhoneAssignmentManager
                .GetCurrentPhoneAssignmentsForActiveShift(CancellationToken.None);

            _activeShift = ResolveActiveShift();
            _lastUpdated = DateTime.Now;
        }
        catch (Exception)
        {
            // Show error state without crashing the dashboard
            _hasError = true;
        }
        finally
        {
            _isLoading = false;
        }
    }

    /// <summary>
    /// Resolves the active shift label based on current system time.
    /// Day (D): 07:00-14:59, Evening (A): 15:00-22:59, Night (N): 23:00-06:59.
    /// </summary>
    private static string ResolveActiveShift()
    {
        int hour = DateTime.Now.Hour;

        return hour switch
        {
            >= 7 and < 15 => "D",
            >= 15 and < 23 => "A",
            _ => "N"
        };
    }

    public void Dispose()
    {
        _refreshTimer?.Dispose();
        GC.SuppressFinalize(this);
    }

    #endregion
}
