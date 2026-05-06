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
}
