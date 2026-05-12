// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.AspNetCore.Components;

namespace WebUI.Client.Components.Residents;

public partial class DeleteConfirmationModal
{
    [Parameter] public bool Show { get; set; }
    [Parameter] public Resident? ResidentToDelete { get; set; }
    [Parameter] public bool IsSaving { get; set; }
    [Parameter] public string? DeleteError { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnDelete { get; set; }
}
