// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Retention;
using Core.Interfaces.Services;

using Domain.Enums;

using Microsoft.AspNetCore.Components;

namespace WebUI.Components.Pages.Gdpr;

/// <summary>
/// Retention Settings page — Admin configures GDPR retention periods (UC-010).
/// </summary>
public partial class RetentionSettingsPage : ComponentBase
{
    #region Injected Services

    [Inject]
    private IRetentionPolicyService RetentionPolicyService { get; set; } = default!;

    #endregion

    #region Fields

    private List<RetentionPolicyDto> _policies = [];
    private bool _isLoading = true;
    private RetentionDataCategory? _editingCategory;
    private int _editDays;
    private string _feedbackMessage = string.Empty;
    private string _feedbackClass = string.Empty;

    #endregion

    #region Lifecycle

    protected override async Task OnInitializedAsync()
    {
        await LoadPoliciesAsync();
    }

    #endregion

    #region Methods

    private async Task LoadPoliciesAsync()
    {
        _isLoading = true;
        try
        {
            IEnumerable<RetentionPolicyDto> result = await RetentionPolicyService.GetPoliciesAsync(CancellationToken.None);
            _policies = [.. result];
        }
        catch (Exception ex)
        {
            ShowFeedback($"Failed to load policies: {ex.Message}", isError: true);
        }
        finally
        {
            _isLoading = false;
        }
    }

    private void StartEdit(RetentionPolicyDto policy)
    {
        _editingCategory = policy.Category;
        _editDays = (int)policy.RetentionPeriod.TotalDays;
        ClearFeedback();
    }

    private void CancelEdit()
    {
        _editingCategory = null;
        _editDays = 0;
    }

    private async Task SavePolicyAsync(RetentionPolicyDto policy)
    {
        int legalMinimumDays = (int)policy.LegalMinimum.TotalDays;
        if (_editDays < legalMinimumDays)
        {
            ShowFeedback($"Retention period cannot be below the legal minimum of {legalMinimumDays} days.", isError: true);
            return;
        }

        try
        {
            UpdateRetentionPolicyDto dto = new()
            {
                Category = policy.Category,
                RetentionPeriod = TimeSpan.FromDays(_editDays),
                Reason = "Admin update via UI"
            };

            // Placeholder employee id — should be retrieved from current authenticated user context
            Guid changedByEmployeeId = Guid.Empty;
            await RetentionPolicyService.UpdateRetentionPolicyAsync(dto, changedByEmployeeId, CancellationToken.None);

            ShowFeedback($"Retention period for {policy.Category} updated to {_editDays} days.", isError: false);
            CancelEdit();
            await LoadPoliciesAsync();
        }
        catch (Exception ex)
        {
            ShowFeedback($"Failed to update policy: {ex.Message}", isError: true);
        }
    }

    private void ShowFeedback(string message, bool isError)
    {
        _feedbackMessage = message;
        _feedbackClass = isError ? "alert-danger" : "alert-success";
    }

    private void ClearFeedback()
    {
        _feedbackMessage = string.Empty;
        _feedbackClass = string.Empty;
    }

    #endregion
}
