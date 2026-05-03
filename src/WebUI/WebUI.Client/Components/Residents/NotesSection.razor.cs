// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

using Domain.Entities;

using Microsoft.AspNetCore.Components;

namespace WebUI.Client.Components.Residents;

public partial class NotesSection
{
    [Parameter] public Resident Resident { get; set; } = default!;
    [Parameter] public List<ResidentNote> Notes { get; set; } = [];
    [Inject] private Core.Interfaces.Services.IResidentNoteService ResidentNoteService { get; set; } = default!;

    private bool _showAddForm;
    private string _newNoteText = string.Empty;
    private Guid? _editingNoteId;
    private string _editNoteText = string.Empty;
    private Guid? _confirmDeleteId;
    private string _feedbackMessage = string.Empty;
    private string _feedbackClass = string.Empty;

    protected override void OnParametersSet()
    {
        // Optionally reset state if Resident changes
        _showAddForm = false;
        _newNoteText = string.Empty;
        _editingNoteId = null;
        _editNoteText = string.Empty;
        _confirmDeleteId = null;
        _feedbackMessage = string.Empty;
        _feedbackClass = string.Empty;
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

        bool success = await ResidentNoteService.AddAsync(Resident.Id, _newNoteText, CancellationToken.None);
        if (success)
        {
            _showAddForm = false;
            _newNoteText = string.Empty;
            SetFeedback("Note gemt.", "alert-success");
            await ReloadNotes();
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

        bool success = await ResidentNoteService.UpdateAsync(noteId, _editNoteText, CancellationToken.None);
        if (success)
        {
            _editingNoteId = null;
            _editNoteText = string.Empty;
            SetFeedback("Note opdateret.", "alert-success");
            await ReloadNotes();
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

        bool success = await ResidentNoteService.DeleteAsync(_confirmDeleteId.Value, CancellationToken.None);
        if (success)
        {
            _confirmDeleteId = null;
            SetFeedback("Note slettet.", "alert-success");
            await ReloadNotes();
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

    private async Task ReloadNotes()
    {
        IEnumerable<ResidentNoteDto> notes = await ResidentNoteService.GetAllByResidentIdAsync(Resident.Id, CancellationToken.None);
        Notes.Clear();
        Notes.AddRange(notes.Select(n => new ResidentNote
        {
            Id = n.Id,
            Note = n.Note,
            EditedAt = n.Timestamp,
            ResidentId = Resident.Id
        }));
        StateHasChanged();
    }
}
