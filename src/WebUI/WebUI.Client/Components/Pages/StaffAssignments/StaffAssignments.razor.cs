// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.
using System.Security.Claims;

using Microsoft.AspNetCore.Components.Authorization;

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
    private AuthenticationStateProvider AuthenticationStateProvider
    {
        get;
        set;
    } = default!;

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

    private string? _selectedEmployeeId;
    private string? _selectedResidentId;

    private ClaimsPrincipal? _user;

    private string? _assignmentError;

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

    // Method that loads employee data from the API
    private async Task LoadEmployeesAsync()
    {
        try
        {
            HttpClient client = HttpClientFactory.CreateClient("SlottetApi");

            // Send a GET request to the "employees" endpoint
            // and convert the JSON response into a collection of EmployeeDto object
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

    // Creates a new staff assignment by sending a POST request to the API.
    private async Task CreateAssignmentAsync()
    {
        _assignmentError =
      $"Resident: {_selectedResidentId} | Employee: {_selectedEmployeeId}";

        if (string.IsNullOrWhiteSpace(_selectedEmployeeId))
        {
            _assignmentError = "Please select a resident.";
            return;
        }

        if (_selectedEmployeeId is null)
        {
            _assignmentError = "Please select an employee.";
            return;
        }

       

        try
        {
            HttpClient client = HttpClientFactory.CreateClient("SlottetApi");

            StaffAssignmentDto dto = new()
            {
                ResidentId = Guid.Parse(_selectedResidentId!),
                EmployeeId = Guid.Parse(_selectedEmployeeId!),
                ShiftType = _selectedShiftType,
                AssignmentDate = _selectedDate
            };

            HttpResponseMessage response =
                await client.PostAsJsonAsync("staff-assignments", dto);

            if (response.IsSuccessStatusCode)
            {
                await LoadAssignmentsAsync();
            }
            else
            {
                string error = await response.Content.ReadAsStringAsync();
                Console.WriteLine(error);

                _hasError = true;
            }
        }
        catch (Exception)
        {
            _hasError = true;
        }
    }

    // Initializes the component by loading the current user's authentication state and fetching residents, employees, and assignments.
    protected override async Task OnInitializedAsync()
    {
        // Get the current authentication state of the user
        AuthenticationState authState =
            await AuthenticationStateProvider.GetAuthenticationStateAsync();

        _user = authState.User;

        await LoadResidentsAsync();
        await LoadEmployeesAsync();
        await LoadAssignmentsAsync();
    }

}
