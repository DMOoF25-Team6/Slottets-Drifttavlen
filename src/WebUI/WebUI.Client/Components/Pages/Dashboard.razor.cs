// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

namespace WebUI.Client.Components.Pages;

public partial class Dashboard
{
    private IEnumerable<Resident> _residents = [];

    protected override async Task OnInitializedAsync()
    {
        _residents = [.. await ResidentService.GetAllAsync()];
    }
}
