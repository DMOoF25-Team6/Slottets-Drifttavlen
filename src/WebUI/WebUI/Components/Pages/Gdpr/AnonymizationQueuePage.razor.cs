// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Anonymization;
using Core.Interfaces.Services;

using Domain.Enums;

using Microsoft.AspNetCore.Components;

namespace WebUI.Components.Pages.Gdpr;

/// <summary>
/// Anonymization Queue page — Admin reviews candidates and approves/rejects anonymization (UC-010).
/// </summary>
public partial class AnonymizationQueuePage : ComponentBase
{
    #region Injected Services

    [Inject]
    private IAnonymizationService AnonymizationService { get; set; } = default!;

    #endregion

    #region Fields

    private List<AnonymizationCandidateDto> _candidates = [];
    private bool _isLoading = true;
    private Guid? _confirmingCandidateId;
    private Guid? _rejectingCandidateId;
    private string _rejectReason = string.Empty;
    private string _feedbackMessage = string.Empty;
    private string _feedbackClass = string.Empty;

    #endregion

    #region Lifecycle

    protected override async Task OnInitializedAsync()
    {
        await LoadCandidatesAsync();
    }

    #endregion

    #region Methods

    private async Task LoadCandidatesAsync()
    {
        _isLoading = true;
        try
        {
            IEnumerable<AnonymizationCandidateDto> result = await AnonymizationService.GetCandidatesAsync(CancellationToken.None);
            _candidates = [.. result];
        }
        catch (Exception ex)
        {
            ShowFeedback($"Failed to load candidates: {ex.Message}", isError: true);
        }
        finally
        {
            _isLoading = false;
        }
    }

    private void StartApprove(Guid candidateId)
    {
        _confirmingCandidateId = candidateId;
        _rejectingCandidateId = null;
        ClearFeedback();
    }

    private void StartReject(Guid candidateId)
    {
        _rejectingCandidateId = candidateId;
        _confirmingCandidateId = null;
        _rejectReason = string.Empty;
        ClearFeedback();
    }

    private void CancelConfirm()
    {
        _confirmingCandidateId = null;
        _rejectingCandidateId = null;
        _rejectReason = string.Empty;
    }

    private async Task ConfirmApproveAsync(Guid candidateId)
    {
        try
        {
            AnonymizationResultDto result = await AnonymizationService.ApproveAnonymizationAsync(candidateId, CancellationToken.None);
            ShowFeedback($"Candidate approved: {result.Outcome}", isError: false);
            CancelConfirm();
            await LoadCandidatesAsync();
        }
        catch (Exception ex)
        {
            ShowFeedback($"Failed to approve: {ex.Message}", isError: true);
        }
    }

    private async Task ConfirmRejectAsync(Guid candidateId)
    {
        if (string.IsNullOrWhiteSpace(_rejectReason))
        {
            ShowFeedback("Rejection reason is required.", isError: true);
            return;
        }

        try
        {
            bool success = await AnonymizationService.RejectAnonymizationAsync(candidateId, _rejectReason, CancellationToken.None);
            if (success)
            {
                ShowFeedback("Candidate rejected.", isError: false);
                CancelConfirm();
                await LoadCandidatesAsync();
            }
            else
            {
                ShowFeedback("Failed to reject candidate.", isError: true);
            }
        }
        catch (Exception ex)
        {
            ShowFeedback($"Failed to reject: {ex.Message}", isError: true);
        }
    }

    private static string GetStatusBadgeClass(AnonymizationStatus status) => status switch
    {
        AnonymizationStatus.Pending => "bg-warning text-dark",
        AnonymizationStatus.Approved => "bg-success",
        AnonymizationStatus.Rejected => "bg-danger",
        AnonymizationStatus.Postponed => "bg-info text-dark",
        AnonymizationStatus.OnHold => "bg-secondary",
        AnonymizationStatus.Completed => "bg-primary",
        _ => "bg-light text-dark"
    };

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
