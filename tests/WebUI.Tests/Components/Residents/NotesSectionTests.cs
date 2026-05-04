// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using AngleSharp.Dom;

using Bunit;

using Core.Interfaces.Services;

using Domain.Entities;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using NotesSectionComponent = WebUI.Client.Components.Residents.NotesSection;

namespace WebUI.Tests.Components.Residents;

/// <summary>
/// Unit tests for the NotesSection Blazor component.
/// </summary>
public class NotesSectionTests : Bunit.TestContext
{
    #region Fields

    private readonly Mock<IResidentNoteService> _residentNoteServiceMock;
    private readonly Resident _resident;

    #endregion

    #region Constructor

    public NotesSectionTests()
    {
        _residentNoteServiceMock = new Mock<IResidentNoteService>();

        _ = Services.AddScoped<IResidentNoteService>(_ => _residentNoteServiceMock.Object);

        _ = _residentNoteServiceMock
            .Setup(s => s.GetAllByResidentIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        _resident = new Resident
        {
            Id = Guid.NewGuid(),
            Initials = "AB",
            Notes = []
        };
    }

    #endregion

    #region Helpers

    private IRenderedComponent<NotesSectionComponent> RenderWithNotes(List<ResidentNote>? notes = null)
    {
        return RenderComponent<NotesSectionComponent>(parameters => parameters
            .Add(p => p.Resident, _resident)
            .Add(p => p.Notes, notes ?? []));
    }

    private static void ClickButtonByText(IRenderedComponent<NotesSectionComponent> cut, string text)
    {
        cut.FindAll("button").First(b => b.TextContent.Contains(text)).Click();
    }

    #endregion

    #region Render Tests

    [Fact]
    public void Render_WithNoNotes_RendersAddButton()
    {
        IRenderedComponent<NotesSectionComponent> cut = RenderWithNotes();

        Assert.Contains("Tilf", cut.Markup);
    }

    [Fact]
    public void Render_WithNotes_RendersNoteContent()
    {
        List<ResidentNote> notes =
        [
            new ResidentNote
            {
                Id = Guid.NewGuid(),
                Note = "Test note content",
                ResidentId = _resident.Id,
                EditedAt = DateTime.UtcNow
            }
        ];

        IRenderedComponent<NotesSectionComponent> cut = RenderWithNotes(notes);

        Assert.Contains("Test note content", cut.Markup);
    }

    #endregion

    #region Toggle Form Tests

    [Fact]
    public void ToggleAddForm_OnFirstClick_ShowsTextarea()
    {
        IRenderedComponent<NotesSectionComponent> cut = RenderWithNotes();

        cut.Find("button.btn-outline-primary").Click();

        Assert.NotNull(cut.Find("textarea"));
    }

    [Fact]
    public void ToggleAddForm_OnSecondClick_HidesTextarea()
    {
        IRenderedComponent<NotesSectionComponent> cut = RenderWithNotes();

        cut.Find("button.btn-outline-primary").Click();
        cut.Find("button.btn-outline-primary").Click();

        Assert.Empty(cut.FindAll("textarea"));
    }

    #endregion

    #region Add Note Tests

    [Fact]
    public void AddNote_WithValidText_CallsServiceAddAsync()
    {
        _ = _residentNoteServiceMock
            .Setup(s => s.AddAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        IRenderedComponent<NotesSectionComponent> cut = RenderWithNotes();

        cut.Find("button.btn-outline-primary").Click();
        cut.Find("textarea").Change("My new note");
        ClickButtonByText(cut, "Gem");

        _residentNoteServiceMock.Verify(
            s => s.AddAsync(_resident.Id, "My new note", It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public void AddNote_WithWhitespaceText_DoesNotCallService()
    {
        IRenderedComponent<NotesSectionComponent> cut = RenderWithNotes();

        cut.Find("button.btn-outline-primary").Click();
        cut.Find("textarea").Change("   ");
        ClickButtonByText(cut, "Gem");

        _residentNoteServiceMock.Verify(
            s => s.AddAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public void AddNote_WhenServiceReturnsTrue_ShowsSuccessFeedback()
    {
        _ = _residentNoteServiceMock
            .Setup(s => s.AddAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        IRenderedComponent<NotesSectionComponent> cut = RenderWithNotes();

        cut.Find("button.btn-outline-primary").Click();
        cut.Find("textarea").Change("Successful note");
        ClickButtonByText(cut, "Gem");

        Assert.Contains("alert-success", cut.Markup);
    }

    [Fact]
    public void AddNote_WhenServiceReturnsFalse_ShowsErrorFeedback()
    {
        _ = _residentNoteServiceMock
            .Setup(s => s.AddAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        IRenderedComponent<NotesSectionComponent> cut = RenderWithNotes();

        cut.Find("button.btn-outline-primary").Click();
        cut.Find("textarea").Change("Failing note");
        ClickButtonByText(cut, "Gem");

        Assert.Contains("alert-danger", cut.Markup);
    }

    #endregion

    #region Cancel Add Form Tests

    [Fact]
    public void CancelAddForm_HidesTextarea()
    {
        IRenderedComponent<NotesSectionComponent> cut = RenderWithNotes();

        cut.Find("button.btn-outline-primary").Click();
        ClickButtonByText(cut, "Annull");

        Assert.Empty(cut.FindAll("textarea"));
    }

    #endregion
}
