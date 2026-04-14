// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Components;

namespace WebUI.Components.Residents;

public partial class ResidentCard : ComponentBase
{
    [Parameter]
    public Resident Resident { get; set; } = default!;

    private static string GetTrafficLightClass(TrafficLightStatus? trafficLight)
    {
        return trafficLight switch
        {
            TrafficLightStatus.Green => "resident-green",
            TrafficLightStatus.Yellow => "resident-yellow",
            TrafficLightStatus.Red => "resident-red",
            _ => "resident-default"
        };
    }
}
