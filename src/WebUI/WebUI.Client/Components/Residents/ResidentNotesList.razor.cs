// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.AspNetCore.Components;

namespace WebUI.Client.Components.Residents;

public partial class ResidentNotesList : ComponentBase
{
    [Parameter]
    public IEnumerable<ResidentNote>? Notes { get; set; }
    [Parameter]
    public Guid? EditingNoteId { get; set; }
    [Parameter]
    public string? EditNoteText { get; set; }
    [Parameter]
    public EventCallback<Guid> OnSaveEdit { get; set; }
    [Parameter]
    public EventCallback OnCancelEdit { get; set; }
    [Parameter]
    public EventCallback<ResidentNote> OnStartEdit { get; set; }
    [Parameter]
    public EventCallback<Guid> OnConfirmDelete { get; set; }
}
