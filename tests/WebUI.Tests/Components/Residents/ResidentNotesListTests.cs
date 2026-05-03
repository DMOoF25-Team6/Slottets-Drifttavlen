// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Bunit;

using Domain.Entities;

using ResidentNotesListComponent = WebUI.Client.Components.Residents.ResidentNotesList;

namespace WebUI.Tests.Components.Residents;

/// <summary>
/// Unit tests for the ResidentNotesList Blazor component.
/// </summary>
public class ResidentNotesListTests : Bunit.TestContext
{
    #region Render Tests

    [Fact]
    public void Render_WithNullNotes_RendersEmptyMessage()
    {
        IRenderedComponent<ResidentNotesListComponent> cut = RenderComponent<ResidentNotesListComponent>(parameters => parameters
            .Add(p => p.Notes, null));

        Assert.Contains("Ingen noter", cut.Markup);
    }

    [Fact]
    public void Render_WithEmptyNotes_RendersEmptyMessage()
    {
        IRenderedComponent<ResidentNotesListComponent> cut = RenderComponent<ResidentNotesListComponent>(parameters => parameters
            .Add(p => p.Notes, []));

        Assert.Contains("Ingen noter", cut.Markup);
    }

    [Fact]
    public void Render_WithNotes_RendersNoteText()
    {
        List<ResidentNote> notes =
        [
            new ResidentNote
            {
                Id = Guid.NewGuid(),
                Note = "First note text",
                EditedAt = DateTime.UtcNow
            }
        ];

        IRenderedComponent<ResidentNotesListComponent> cut = RenderComponent<ResidentNotesListComponent>(parameters => parameters
            .Add(p => p.Notes, notes));

        Assert.Contains("First note text", cut.Markup);
    }

    [Fact]
    public void Render_WithMultipleNotes_RendersInDescendingOrder()
    {
        DateTime older = new(2026, 5, 1, 10, 0, 0, DateTimeKind.Utc);
        DateTime newer = new(2026, 5, 4, 10, 0, 0, DateTimeKind.Utc);

        List<ResidentNote> notes =
        [
            new ResidentNote
            {
                Id = Guid.NewGuid(),
                Note = "Older note",
                EditedAt = older
            },
            new ResidentNote
            {
                Id = Guid.NewGuid(),
                Note = "Newer note",
                EditedAt = newer
            }
        ];

        IRenderedComponent<ResidentNotesListComponent> cut = RenderComponent<ResidentNotesListComponent>(parameters => parameters
            .Add(p => p.Notes, notes));

        int newerIndex = cut.Markup.IndexOf("Newer note");
        int olderIndex = cut.Markup.IndexOf("Older note");

        Assert.True(newerIndex < olderIndex, "Newer note should appear before older note in markup.");
    }

    [Fact]
    public void Render_InEditMode_RendersTextarea()
    {
        Guid noteId = Guid.NewGuid();

        List<ResidentNote> notes =
        [
            new ResidentNote
            {
                Id = noteId,
                Note = "Editable note",
                EditedAt = DateTime.UtcNow
            }
        ];

        IRenderedComponent<ResidentNotesListComponent> cut = RenderComponent<ResidentNotesListComponent>(parameters => parameters
            .Add(p => p.Notes, notes)
            .Add(p => p.EditingNoteId, noteId)
            .Add(p => p.EditNoteText, "Editing this"));

        Assert.NotNull(cut.Find("textarea"));
    }

    [Fact]
    public void Render_NotInEditMode_DoesNotRenderTextarea()
    {
        List<ResidentNote> notes =
        [
            new ResidentNote
            {
                Id = Guid.NewGuid(),
                Note = "View only note",
                EditedAt = DateTime.UtcNow
            }
        ];

        IRenderedComponent<ResidentNotesListComponent> cut = RenderComponent<ResidentNotesListComponent>(parameters => parameters
            .Add(p => p.Notes, notes)
            .Add(p => p.EditingNoteId, null));

        Assert.Empty(cut.FindAll("textarea"));
    }

    #endregion

    #region Event Tests

    [Fact]
    public void ClickEditButton_RaisesOnStartEditEvent()
    {
        ResidentNote noteToEdit = new()
        {
            Id = Guid.NewGuid(),
            Note = "Edit me",
            EditedAt = DateTime.UtcNow
        };

        ResidentNote? receivedNote = null;

        IRenderedComponent<ResidentNotesListComponent> cut = RenderComponent<ResidentNotesListComponent>(parameters => parameters
            .Add(p => p.Notes, [noteToEdit])
            .Add(p => p.OnStartEdit, n => receivedNote = n));

        cut.FindAll("button").First(b => b.TextContent.Contains("Rediger")).Click();

        Assert.NotNull(receivedNote);
        Assert.Equal(noteToEdit.Id, receivedNote!.Id);
    }

    [Fact]
    public void ClickDeleteButton_RaisesOnConfirmDeleteEvent()
    {
        Guid noteId = Guid.NewGuid();

        ResidentNote noteToDelete = new()
        {
            Id = noteId,
            Note = "Delete me",
            EditedAt = DateTime.UtcNow
        };

        Guid? receivedId = null;

        IRenderedComponent<ResidentNotesListComponent> cut = RenderComponent<ResidentNotesListComponent>(parameters => parameters
            .Add(p => p.Notes, [noteToDelete])
            .Add(p => p.OnConfirmDelete, id => receivedId = id));

        cut.FindAll("button").First(b => b.TextContent.Contains("Slet")).Click();

        Assert.Equal(noteId, receivedId);
    }

    #endregion
}
