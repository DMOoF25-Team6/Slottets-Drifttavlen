// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.AspNetCore.Components;

namespace WebUI.Client.Components.Residents;

public partial class MedicineStatus
{
    #region Parameters
    [Parameter]
    public dynamic? MedicineStatusData { get; set; }
    #endregion
    #region Injected Services
    #endregion

    #region Fields
    private readonly List<Resident> _residents = [];
    #endregion

    #region Lifecycle
    protected override Task OnInitializedAsync()
    {
        return Task.CompletedTask;
    }
    #endregion

    #region Methods
    #endregion
}
