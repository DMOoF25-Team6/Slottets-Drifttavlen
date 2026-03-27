using System;
using System.Collections.Generic;
using System.Linq;

using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Components;

namespace WebUI.Components.Residents;

public partial class ResidentCard : ComponentBase
{
    private List<Resident> residents = new List<Resident>
    {
        new Resident
        {
            Id = Guid.NewGuid(),
            Initials = "AB",
            TrafficLight = TrafficLight.Green,
            Notes = new List<ResidentNote>
            {
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
            }
        },

        new Resident
        {
            Initials = "CD",
            TrafficLight = TrafficLight.Yellow,
            Notes = new List<ResidentNote>
            {
                new ResidentNote
                {
                    Id = Guid.NewGuid(),
                    Note = "Virker lidt urolig før frokost."
                }
            }
        },
        new Resident { Initials = "EF", TrafficLight = TrafficLight.Red },
        new Resident { Initials = "DH", TrafficLight = TrafficLight.Green },
        new Resident { Initials = "JC", TrafficLight = TrafficLight.Yellow },
        new Resident { Initials = "GD", TrafficLight = TrafficLight.Red },
        new Resident { Initials = "YX", TrafficLight = TrafficLight.Green },
        new Resident { Initials = "ZB", TrafficLight = TrafficLight.Yellow },
        new Resident { Initials = "HU", TrafficLight = TrafficLight.Red },
        new Resident { Initials = "RD", TrafficLight = TrafficLight.Green },
        new Resident { Initials = "MD", TrafficLight = TrafficLight.Yellow },
        new Resident { Initials = "IB", TrafficLight = TrafficLight.Red }
    };

    private string GetTrafficLightClass(TrafficLight? trafficLight)
    {
        return trafficLight switch
        {
            TrafficLight.Green => "resident-green",
            TrafficLight.Yellow => "resident-yellow",
            TrafficLight.Red => "resident-red",
            _ => "resident-default"
        };
    }
}
