// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


using Core.DTOs;
using Core.Interfaces.Services;

using System.Net.Http.Json;

using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Components;


namespace WebUI.Client.Components.Pages.StaffAssignments;


/// <summary>
/// Page for viewing and managing staff assignments for residents during shifts.
/// </summary>
public partial class StaffAssignments : ComponentBase
{
    

    [Inject]
    private IHttpClientFactory HttpClientFactory { get; set; } = default!;

    [Inject]
    private IResidentService ResidentService { get; set; } = default!;

    // Stores the list of assignments shown by the component.
    private IReadOnlyList<AssignmentOverviewDto> _assignments = [];
    private IReadOnlyList<Resident> _residents = [];
    private IReadOnlyList<EmployeeDto> _employees = [];

    //while data is being loaded.
    private bool _isLoading;

    // True if something goes wrong while loading assignments.
    private bool _hasError;

    private ShiftType _selectedShiftType= ShiftType.Day;
    private DateTime _selectedDate = DateTime.Today;

    private Guid? _selectedEmployeeId;


    // Loads assignments based on the selected date and shift type.
    private async Task LoadAssignmentsAsync()
    {
        _isLoading = true;
        _hasError = false;

        try
        {
            HttpClient client = HttpClientFactory.CreateClient("SlottetApi");

            IEnumerable<AssignmentOverviewDto>? assignments =
                await client.GetFromJsonAsync<IEnumerable<AssignmentOverviewDto>>(
                    $"staff-assignments/list?shiftType={(int)_selectedShiftType}&assignmentDate={_selectedDate:yyyy-MM-dd}");

            _assignments = assignments?.ToList() ?? [];
        }
        catch (Exception)
        {
            _hasError = true;
            _assignments = [];
        }
        finally
        {
            _isLoading = false;
        }
    }

    private async Task LoadResidentsAsync()
    {
        try
        {
            IEnumerable<Resident> residents =
                await ResidentService.GetAllAsync();

            _residents = [.. residents];
        }
        catch (Exception)
        {
            _hasError = true;
            _residents = [];
        }
    }

    private async Task LoadEmployeesAsync()
    {
        try
        {
            HttpClient client = HttpClientFactory.CreateClient("SlottetApi");

            IEnumerable<EmployeeDto>? employees =
                await client.GetFromJsonAsync<IEnumerable<EmployeeDto>>(
                    "employees");

            _employees = employees?.ToList() ?? [];
        }
        catch (Exception)
        {
            _hasError = true;
            _employees = [];
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadResidentsAsync();
        await LoadEmployeesAsync();
        await LoadAssignmentsAsync();

    }

}
