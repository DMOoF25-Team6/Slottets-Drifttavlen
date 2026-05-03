// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

// src/WebUI/WebUI.Client/Components/Residents/PainkillerStatus.razor.cs

using Core.DTOs;

using Microsoft.AspNetCore.Components;

namespace WebUI.Client.Components.Residents;

public partial class PainkillerStatus : ComponentBase
{
    /// <summary>
    /// The painkiller status data for the resident.
    /// </summary>
    [Parameter]
    public PainkillerStatusDto? Status { get; set; }
}
