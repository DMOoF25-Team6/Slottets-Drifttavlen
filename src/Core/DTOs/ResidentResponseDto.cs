// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs;

public class ResidentResponseDto
{
    // gets or sets the unique identifier of the resident

    public Guid Id { get; set; }

    // gets or sets the name of the resident
    public string Initials { get; set; } = string.Empty;

    //gets or sets the status of the resident's traffic light, where 0 = green, 1 = yellow, 2 = red, and null = no status
    public int? TrafficLightStatus { get; set; }

    //gets or sets the list of resident notes associated with the resident, which may be empty if there are no notes
    public List<ResidentNoteDto> Notes { get; set; } = [];
}
