// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Microsoft.AspNetCore.Components;

using WebUI.Client.Models;

namespace WebUI.Client.Components.Residents;

public partial class ResidentFormModal
{
    [Parameter] public bool IsEditing { get; set; }
    [Parameter] public bool IsSaving { get; set; }
    [Parameter] public ResidentFormModel FormModel { get; set; } = default!;
    [Parameter] public string? FormError { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnValidSubmit { get; set; }

    private async Task OnValidSubmitHandler()
    {
        await OnValidSubmit.InvokeAsync(null);
    }
}