// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

using Core.DTOs;
using Core.Interfaces.Services;

using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebUI.Client.Components.Pages.Residents;

/// <summary>
/// Administration page for managing residents (CRUD operations).
/// </summary>
public partial class Residents : ComponentBase
{
    #region Injected Services

    [Inject]
    private IResidentService ResidentService { get; set; } = default!;

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    #endregion

    #region Fields

    private IReadOnlyList<Resident> _residents = [];

    private bool _isLoading = true;
    private bool _hasError;
    private bool _isSaving;

    private string _selectedDepartment = Department.Slottet.ToString();

    private static readonly string[] DepartmentOptions =
        [Department.Slottet.ToString(), Department.Skoven.ToString(), Department.Marken.ToString()];

    private IEnumerable<Resident> FilteredResidents =>
        _residents.Where(r => r.Department.ToString() == _selectedDepartment);

    // Authorization state derived from JWT claims
    private bool _hasManageClaim;
    private string? _userDepartment; // null = unrestricted (admin), value = restricted to that department

    /// <summary>True when the current user may manage residents in the currently selected department tab.</summary>
    private bool CanManageCurrentDepartment =>
        _hasManageClaim && (_userDepartment is null || _userDepartment == _selectedDepartment);

    /// <summary>True when the current user may manage a specific resident's department.</summary>
    private bool CanManageResident(Resident resident) =>
        _hasManageClaim && (_userDepartment is null || _userDepartment == resident.Department.ToString());

    // Form modal state
    private bool _showFormModal;
    private bool _isEditing;
    private Guid _editingId;
    private ResidentFormModel _formModel = new();
    private string? _formError;

    // Delete modal state
    private bool _showDeleteModal;
    private Resident? _residentToDelete;
    private string? _deleteError;

    #endregion

    #region Lifecycle

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        ClaimsPrincipal user = authState.User;

        _hasManageClaim = user.HasClaim(
            c => c.Type == ClaimTypes.Role && c.Value == "CanManageResidents");

        // Null means the user has no department restriction (admin).
        _userDepartment = user.FindFirstValue("Department");

        await LoadResidentsAsync();
    }

    #endregion

    #region Data Loading

    private async Task LoadResidentsAsync()
    {
        _isLoading = true;
        _hasError = false;

        try
        {
            IEnumerable<Resident> residents = await ResidentService.GetAllAsync();
            _residents = [.. residents];
        }
        catch
        {
            _hasError = true;
        }
        finally
        {
            _isLoading = false;
        }
    }

    #endregion

    #region Department Filter

    private void SelectDepartment(string department)
    {
        _selectedDepartment = department;
    }

    #endregion

    #region Create / Edit Modal

    private void OpenCreateModal()
    {
        _isEditing = false;
        _editingId = Guid.Empty;
        _formModel = new ResidentFormModel
        {
            Department = Enum.TryParse<Department>(_selectedDepartment, out Department dept) ? dept : Department.Slottet
        };
        _formError = null;
        _showFormModal = true;
    }

    private void OpenEditModal(Resident resident)
    {
        _isEditing = true;
        _editingId = resident.Id;
        _formModel = new ResidentFormModel
        {
            Initials = resident.Initials,
            FirstName = resident.FirstName,
            LastName = resident.LastName,
            TrafficLightStatus = resident.TrafficLightStatus,
            Department = resident.Department
        };
        _formError = null;
        _showFormModal = true;
    }

    private void CloseFormModal()
    {
        _showFormModal = false;
    }

    private async Task HandleFormSubmitAsync()
    {
        _isSaving = true;
        _formError = null;

        try
        {
            if (_isEditing)
            {
                await ResidentService.UpdateAsync(new ResidentUpdateRequestDto
                {
                    Initials = _formModel.Initials,
                    FirstName = _formModel.FirstName,
                    LastName = _formModel.LastName,
                    TrafficLightStatus = _formModel.TrafficLightStatus,
                    Department = _formModel.Department
                });
            }
            else
            {
                await ResidentService.CreateAsync(new ResidentCreateRequestDto
                {
                    Initials = _formModel.Initials,
                    FirstName = _formModel.FirstName,
                    LastName = _formModel.LastName,
                    TrafficLightStatus = _formModel.TrafficLightStatus,
                    Department = _formModel.Department
                });
            }

            _showFormModal = false;
            await LoadResidentsAsync();
        }
        catch (Exception ex)
        {
            _formError = ex.Message;
        }
        finally
        {
            _isSaving = false;
        }
    }

    #endregion

    #region Delete Modal

    private void OpenDeleteConfirm(Resident resident)
    {
        _residentToDelete = resident;
        _deleteError = null;
        _showDeleteModal = true;
    }

    private void CloseDeleteModal()
    {
        _showDeleteModal = false;
        _residentToDelete = null;
    }

    private async Task HandleDeleteAsync()
    {
        if (_residentToDelete is null)
        {
            return;
        }

        _isSaving = true;
        _deleteError = null;

        try
        {
            await ResidentService.DeleteAsync(_residentToDelete.Id);
            _showDeleteModal = false;
            _residentToDelete = null;
            await LoadResidentsAsync();
        }
        catch (Exception ex)
        {
            _deleteError = ex.Message;
        }
        finally
        {
            _isSaving = false;
        }
    }

    #endregion

    #region Helpers

    private static string GetTrafficLightBadgeClass(TrafficLightStatus status) => status switch
    {
        TrafficLightStatus.Green => "bg-success",
        TrafficLightStatus.Yellow => "bg-warning text-dark",
        TrafficLightStatus.Red => "bg-danger",
        _ => "bg-secondary"
    };

    private static string GetTrafficLightLabel(TrafficLightStatus status) => status switch
    {
        TrafficLightStatus.Green => "Grøn",
        TrafficLightStatus.Yellow => "Gul",
        TrafficLightStatus.Red => "Rød",
        _ => "Ukendt"
    };

    #endregion

    #region Nested Types

    /// <summary>Form model with validation attributes for the create/edit modal.</summary>
    private sealed class ResidentFormModel
    {
        [Required(ErrorMessage = "Initialer er påkrævet.")]
        [MaxLength(2, ErrorMessage = "Initialer må højst være 2 tegn.")]
        public string Initials { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fornavn er påkrævet.")]
        [MaxLength(50, ErrorMessage = "Fornavn må højst være 50 tegn.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Efternavn er påkrævet.")]
        [MaxLength(50, ErrorMessage = "Efternavn må højst være 50 tegn.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Afdeling er påkrævet.")]
        public Department Department { get; set; }

        public TrafficLightStatus? TrafficLightStatus { get; set; }
    }

    #endregion
}
