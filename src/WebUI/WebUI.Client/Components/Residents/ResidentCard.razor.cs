// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Services;


using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Components;

namespace WebUI.Client.Components.Residents;

public partial class ResidentCard : ComponentBase
{
    #region Parameters

    [Parameter]
    public Resident Resident { get; set; } = default!;

    #endregion

    #region Injected Services

    [Inject]
    private IResidentNoteService ResidentNoteService { get; set; } = default!;

    [Inject]
    private IMedicineStatusService MedicineStatusService { get; set; } = default!;


    #endregion

    #region Fields

    private bool _showAddForm;
    private string _newNoteText = string.Empty;
    private Guid? _editingNoteId;
    private string _editNoteText = string.Empty;
    private Guid? _confirmDeleteId;
    private string _feedbackMessage = string.Empty;
    private string _feedbackClass = string.Empty;

    private MedicineStatusDto? _medicineStatus;
    private PainkillerStatusDto? _painkillerStatus;

    private string _notesErrorMessage = string.Empty;

    #endregion

    #region Lifecycle

    protected override async Task OnInitializedAsync()
    {
        await LoadNotes();
        await LoadMedicineStatus();
    }

    #endregion

    #region Methods

    private async Task LoadNotes()
    {
        try
        {
            IEnumerable<Core.DTOs.ResidentNoteDto> notes = await ResidentNoteService
                .GetAllByResidentIdAsync(Resident.Id, CancellationToken.None);

            Resident.Notes = [.. notes.Select(n => new ResidentNote
            {
                Id = n.Id,
                Note = n.Note,
                EditedAt = n.Timestamp,
                ResidentId = Resident.Id
            })];
        }
        catch (HttpRequestException ex)
        {
            // Log the error (use your logging framework or Console)
            Console.Error.WriteLine($"Failed to load notes: {ex.Message}");
            // Optionally, set a user-friendly error message for the UI
            _notesErrorMessage = "Kunne ikke hente noter. Prøv igen senere.";
        }
    }

    private void ToggleAddForm()
    {
        _showAddForm = !_showAddForm;
        _newNoteText = string.Empty;
        ClearFeedback();
    }

    private async Task AddNote()
    {
        if (string.IsNullOrWhiteSpace(_newNoteText))
        {
            return;
        }

        bool success = await ResidentNoteService.AddAsync(
            Resident.Id, _newNoteText, CancellationToken.None);

        if (success)
        {
            _showAddForm = false;
            _newNoteText = string.Empty;
            SetFeedback("Note gemt.", "alert-success");
            await LoadNotes();
        }
        else
        {
            SetFeedback("Note kunne ikke gemmes.", "alert-danger");
        }
    }

    private void StartEdit(ResidentNote note)
    {
        _editingNoteId = note.Id;
        _editNoteText = note.Note;
        ClearFeedback();
    }

    private void CancelEdit()
    {
        _editingNoteId = null;
        _editNoteText = string.Empty;
    }

    private async Task SaveEdit(Guid noteId)
    {
        if (string.IsNullOrWhiteSpace(_editNoteText))
        {
            return;
        }

        bool success = await ResidentNoteService.UpdateAsync(
            noteId, _editNoteText, CancellationToken.None);

        if (success)
        {
            _editingNoteId = null;
            _editNoteText = string.Empty;
            SetFeedback("Note opdateret.", "alert-success");
            await LoadNotes();
        }
        else
        {
            SetFeedback("Note kunne ikke opdateres.", "alert-danger");
        }
    }

    private void ConfirmDelete(Guid noteId)
    {
        _confirmDeleteId = noteId;
        ClearFeedback();
    }

    private void CancelDelete()
    {
        _confirmDeleteId = null;
    }

    private async Task DeleteNote()
    {
        if (_confirmDeleteId is null)
        {
            return;
        }

        bool success = await ResidentNoteService.DeleteAsync(
            _confirmDeleteId.Value, CancellationToken.None);

        if (success)
        {
            _confirmDeleteId = null;
            SetFeedback("Note slettet.", "alert-success");
            await LoadNotes();
        }
        else
        {
            SetFeedback("Note kunne ikke slettes.", "alert-danger");
        }
    }

    private void SetFeedback(string message, string cssClass)
    {
        _feedbackMessage = message;
        _feedbackClass = cssClass;
    }

    private void ClearFeedback()
    {
        _feedbackMessage = string.Empty;
        _feedbackClass = string.Empty;
    }

    private static string GetTrafficLightClass(TrafficLightStatus? trafficLight)
    {
        return trafficLight switch
        {
            TrafficLightStatus.Green => "resident-green",
            TrafficLightStatus.Yellow => "resident-yellow",
            TrafficLightStatus.Red => "resident-red",
            _ => "resident-default"
        };
    }

    private async Task LoadMedicineStatus()
    {
        _medicineStatus = await MedicineStatusService.GetMedicineStatusAsync(Resident.Id);
        _painkillerStatus = await MedicineStatusService.GetPainkillerStatusAsync(Resident.Id);

        if (_painkillerStatus is not null)
        {
            _painkillerStatus = new PainkillerStatusDto
            {
                NextAllowedTime = _painkillerStatus.NextAllowedTime,
                Types = _painkillerStatus.Types
            };
        }
    }

    #endregion
}
