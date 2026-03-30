// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs;

public class PainkillerStatusDto
{

    public Guid ResidentId { get; set; }
    public string[] Types { get; set; } = Array.Empty<string>();
    public DateTime NextAllowedTime { get; set; }
}
