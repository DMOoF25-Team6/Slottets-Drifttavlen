// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Audit;
using Core.Interfaces.Managers;

using Microsoft.AspNetCore.Components;

namespace WebUI.Client.Components.Pages.AuditHistory;

/// <summary>
/// Displays a chronological list of audit events with optional entity-type filtering and per-entry change details.
/// </summary>
/// <remarks>
/// Implements UC-009 (View History and Traceability). Audit entries are written automatically by the
/// audit interceptor on <c>SaveChanges</c>; this page only consumes them via the read-only audit endpoints.
/// </remarks>
public partial class AuditHistory : ComponentBase
{
    #region Injected Services

    [Inject]
    private IAuditManager AuditManager { get; set; } = default!;

    #endregion

    #region Fields

    /// <summary>The list of audit entries currently displayed.</summary>
    private IEnumerable<AuditEntryDto> _entries = [];

    /// <summary>The audit entry whose change details are shown in the modal, or <see langword="null"/> when the modal is hidden.</summary>
    private AuditEntryDto? _selectedEntry;

    /// <summary>The entity-name filter selected by the user; <see cref="string.Empty"/> means "show all".</summary>
    private string _selectedEntity = string.Empty;

    /// <summary>The maximum number of entries to retrieve when no entity filter is applied.</summary>
    private int _limit = 50;

    private bool _isLoading = true;
    private bool _isLoadingDetails;
    private bool _hasError;

    /// <summary>Available entity-name options shown in the filter dropdown.</summary>
    private static readonly string[] EntityOptions =
    [
        "Resident",
        "ResidentNote",
        "PhoneAssignment",
        "MedicineRecord",
        "PainkillerRecord"
    ];

    #endregion

    #region Lifecycle

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        await LoadEntriesAsync();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Loads audit entries from the API based on the current filter selection.
    /// </summary>
    private async Task LoadEntriesAsync()
    {
        _isLoading = true;
        _hasError = false;

        try
        {
            _entries = string.IsNullOrEmpty(_selectedEntity)
                ? await AuditManager.GetRecentAsync(_limit, CancellationToken.None)
                : await AuditManager.GetByEntityNameAsync(_selectedEntity, CancellationToken.None);
        }
        catch (Exception)
        {
            _hasError = true;
            _entries = [];
        }
        finally
        {
            _isLoading = false;
        }
    }

    /// <summary>
    /// Reloads the list whenever the user changes the entity filter.
    /// </summary>
    private async Task OnEntityFilterChanged(ChangeEventArgs e)
    {
        _selectedEntity = e.Value?.ToString() ?? string.Empty;
        await LoadEntriesAsync();
    }

    /// <summary>
    /// Reloads the list whenever the user changes the limit value.
    /// </summary>
    private async Task OnLimitChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int parsed) && parsed > 0)
        {
            _limit = parsed;
            await LoadEntriesAsync();
        }
    }

    /// <summary>
    /// Opens the change-details modal and fetches the full <see cref="AuditEntryDto"/> with details.
    /// </summary>
    /// <param name="entryId">The identifier of the audit entry to display.</param>
    private async Task ShowDetailsAsync(Guid entryId)
    {
        _isLoadingDetails = true;
        _selectedEntry = null;

        try
        {
            _selectedEntry = await AuditManager.GetWithDetailsAsync(entryId, CancellationToken.None);
        }
        catch (Exception)
        {
            _selectedEntry = null;
        }
        finally
        {
            _isLoadingDetails = false;
        }
    }

    /// <summary>
    /// Closes the change-details modal.
    /// </summary>
    private void CloseDetails()
    {
        _selectedEntry = null;
    }

    /// <summary>
    /// Returns a Bootstrap badge class that visually distinguishes the change type.
    /// </summary>
    /// <param name="changeType">The change type as recorded by the audit interceptor.</param>
    /// <returns>A Bootstrap badge CSS class.</returns>
    private static string GetBadgeClass(string changeType) => changeType switch
    {
        "Added" => "badge bg-success",
        "Modified" => "badge bg-warning text-dark",
        "Deleted" => "badge bg-danger",
        _ => "badge bg-secondary"
    };

    #endregion
}
