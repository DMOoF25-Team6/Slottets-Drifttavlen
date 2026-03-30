// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Components;

namespace WebUI.Components.Residents;

public partial class ResidentCard : ComponentBase
{
    private readonly List<Resident> residents =
    [
        new Resident
        {
            Id = Guid.NewGuid(),
            Initials = "AB",
            TrafficLightStatus = TrafficLightStatus.Green,
            Notes =
            [
                new ResidentNote
                {
                    Id = Guid.NewGuid(),
                    Note = "sov godt i nat og var i godt humør til morges."
                },
                new ResidentNote
                {
                    Id = Guid.NewGuid(),
                    Note = "stresset da han var ude at handle",
                    EditedAt = DateTime.Now.AddHours(-1)
                }
            ]
        },

        new Resident
        {
            Initials = "CD",
            TrafficLightStatus = TrafficLightStatus.Yellow,
            Notes =
            [
                new ResidentNote
                {
                    Id = Guid.NewGuid(),
                    Note = "Virker lidt urolig før frokost."
                }
            ]
        },
        new Resident { Initials = "EF", TrafficLightStatus = TrafficLightStatus.Red },
        new Resident { Initials = "DH", TrafficLightStatus = TrafficLightStatus.Green },
        new Resident { Initials = "JC", TrafficLightStatus = TrafficLightStatus.Yellow },
        new Resident { Initials = "GD", TrafficLightStatus = TrafficLightStatus.Red },
        new Resident { Initials = "YX", TrafficLightStatus = TrafficLightStatus.Green },
        new Resident { Initials = "ZB", TrafficLightStatus = TrafficLightStatus.Yellow },
        new Resident { Initials = "HU", TrafficLightStatus = TrafficLightStatus.Red },
        new Resident { Initials = "RD", TrafficLightStatus = TrafficLightStatus.Green },
        new Resident { Initials = "MD", TrafficLightStatus = TrafficLightStatus.Yellow },
        new Resident { Initials = "IB", TrafficLightStatus = TrafficLightStatus.Red }
    ];

    private string GetTrafficLightClass(TrafficLightStatus? trafficLight)
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
