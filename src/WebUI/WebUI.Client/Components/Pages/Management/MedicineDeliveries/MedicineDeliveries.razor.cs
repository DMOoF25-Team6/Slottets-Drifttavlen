// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.Net.Http;
using Core.DTOs;
using Core.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebUI.Client.Components.Pages.Management.MedicineDeliveries;

public partial class MedicineDeliveries
{
    #region Fields
    [Inject]
    private IMedicineDeliveryService MedicineDeliveryService { get; set; } = default!;

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    private List<MedicineRecord> _deliveries = new();
    private bool _isLoading = true;
    private bool _hasError;
    private string _errorMessage = string.Empty;

    private bool _showFormModal;
    private bool _isEditing;
    private MedicineDeliveryFormModel _formModel = new();
    private Guid? _editingId;

    private bool _showDeleteModal;
    private MedicineRecord? _selectedDelivery;
    private bool _firstRenderDone;
    #endregion

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender || _firstRenderDone)
        {
            return;
        }
        _firstRenderDone = true;

        // Materialise WASM auth state from localStorage before any API call so the
        // JWT bearer token is attached; mirrors the GDPR pages pattern.
        await InitializeAuthorizationAsync();
        await LoadAsync();
        StateHasChanged();
    }

    private async Task InitializeAuthorizationAsync()
    {
        AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        _ = authState.User;
    }

    private async Task LoadAsync()
    {
        try
        {
            _isLoading = true;
            _hasError = false;
            _deliveries = (await MedicineDeliveryService.GetAllAsync()).ToList();
        }
        catch (HttpRequestException ex)
        {
            _hasError = true;
            _errorMessage = ex.Message;
        }
        catch (TaskCanceledException ex)
        {
            _hasError = true;
            _errorMessage = ex.Message;
        }
        finally
        {
            _isLoading = false;
        }
    }

    private void OpenCreateModal()
    {
        _isEditing = false;
        _editingId = null;
        _formModel = new MedicineDeliveryFormModel { Timestamp = DateTime.UtcNow };
        _showFormModal = true;
    }

    private void OpenEditModal(MedicineRecord d)
    {
        _isEditing = true;
        _editingId = d.Id;
        _formModel = new MedicineDeliveryFormModel
        {
            ResidentId = d.ResidentId,
            MedicineName = d.MedicineName,
            Timestamp = d.Timestamp,
            Given = d.Given
        };
        _showFormModal = true;
    }

    private void CloseFormModal()
    {
        _showFormModal = false;
    }

    private async Task SaveAsync()
    {
        try
        {
            if (_isEditing && _editingId.HasValue)
            {
                MedicineDeliveryUpdateRequestDto dto = new()
                {
                    ResidentId = _formModel.ResidentId,
                    MedicineName = _formModel.MedicineName,
                    Timestamp = _formModel.Timestamp,
                    Given = _formModel.Given
                };
                await MedicineDeliveryService.UpdateAsync(_editingId.Value, dto);
            }
            else
            {
                MedicineDeliveryCreateRequestDto dto = new()
                {
                    ResidentId = _formModel.ResidentId,
                    MedicineName = _formModel.MedicineName,
                    Timestamp = _formModel.Timestamp,
                    Given = _formModel.Given
                };
                await MedicineDeliveryService.CreateAsync(dto);
            }

            _showFormModal = false;
            await LoadAsync();
        }
        catch (HttpRequestException ex)
        {
            _hasError = true;
            _errorMessage = ex.Message;
        }
        catch (TaskCanceledException ex)
        {
            _hasError = true;
            _errorMessage = ex.Message;
        }
    }

    private void OpenDeleteModal(MedicineRecord d)
    {
        _selectedDelivery = d;
        _showDeleteModal = true;
    }

    private void CloseDeleteModal()
    {
        _showDeleteModal = false;
    }

    private async Task DeleteAsync()
    {
        if (_selectedDelivery is null)
        {
            return;
        }
        try
        {
            await MedicineDeliveryService.DeleteAsync(_selectedDelivery.Id);
            _showDeleteModal = false;
            await LoadAsync();
        }
        catch (HttpRequestException ex)
        {
            _hasError = true;
            _errorMessage = ex.Message;
        }
        catch (TaskCanceledException ex)
        {
            _hasError = true;
            _errorMessage = ex.Message;
        }
    }
}

public class MedicineDeliveryFormModel
{
    public Guid ResidentId { get; set; }
    public string MedicineName { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool Given { get; set; }
}
