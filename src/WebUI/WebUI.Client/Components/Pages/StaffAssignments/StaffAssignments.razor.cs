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

    // Stores the ID of the assignment currently being edited.
    private Guid? _editingAssignmentId;

    // Indicates whether the page is currently editing an assignment.
   // private bool _isEditing;

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

    // Creates or updates a nstaff assignment depending on edit state
    private async Task SaveAssignmentAsync()
    {
      

        if (string.IsNullOrWhiteSpace(_selectedResidentId))
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

            // Send a POST request when creating a new assignment.
            HttpResponseMessage response;

            if (_editingAssignmentId is null)
            {
                response =
                    await client.PostAsJsonAsync(
                        "staff-assignments",
                        dto);
            }
            else
            {
                // Send a PUT request when updating an existing assignment.
                response =
                    await client.PutAsJsonAsync(
                        $"staff-assignments/{_editingAssignmentId}",
                        dto);
            }
            if (response.IsSuccessStatusCode)
            {
                await LoadAssignmentsAsync();

                // Reset edit mode after successful save.
                _editingAssignmentId = null;
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


    // Removes an existing staff assignment by sending a DELETE request to the API.
    // If the deletion succeeds, the assignment overview is refreshed automatically.
    private async Task RemoveAssignmentAsync(Guid assignmentId)
    {
        try
        {
            // Create a configured HTTP client for communicating with the API.
            HttpClient client =
                HttpClientFactory.CreateClient("SlottetApi");

            // Send a DELETE request to the API using the assignment ID.
            HttpResponseMessage response =
                await client.DeleteAsync(
                    $"staff-assignments/{assignmentId}");

            // Reload the assignment overview if the deletion succeeds.
            if (response.IsSuccessStatusCode)
            {
                await LoadAssignmentsAsync();
            }
            else
            {
                _hasError = true;
            }
        }
        catch (Exception)
        {
            _hasError = true;
        }
    }


    // Loads an existing assignment into the form for editing.
    private void EditAssignment(AssignmentOverviewDto assignment)
    {
        // Store the ID of the assignment currently being edited.
        _editingAssignmentId = assignment.AssignmentId;

       

        // Populate the form fields with existing assignment values.
        _selectedResidentId = assignment.ResidentId.ToString();
        _selectedEmployeeId = assignment.EmployeeId.ToString();

        // Convert the shift type string back to enum value.
        _selectedShiftType =
            Enum.Parse<ShiftType>(assignment.ShiftType);

        // Load the assignment date into the date picker.
        _selectedDate = assignment.AssignmentDate;
    }

    // Initializes the component by loading the current user's authentication state and fetching residents, employees, and assignments.
    // This method runs after the component has rendered on the screen
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        // Only run this code on the first render to avoid infinite loops
        if (!firstRender)
        {
            return;
        }

        // Get the current authentication state (logged-in user info)
        AuthenticationState authState =
            await AuthenticationStateProvider.GetAuthenticationStateAsync();

        // Save the logged-in user into the _user variable
        _user = authState.User;

        // Load residents, employees, and assignments from the API
        await LoadResidentsAsync();
        await LoadEmployeesAsync();
        await LoadAssignmentsAsync();

        // Refresh the UI after data has loaded
        StateHasChanged();
    }
}
