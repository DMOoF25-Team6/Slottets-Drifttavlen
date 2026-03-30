// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs;

public class MedicineStatusDto
{

    public string ResidentId { get; set; } = string.Empty;
    public string[] Medicine { get; set; } = Array.Empty<string>();
    public DateTime[] Timestamps { get; set; } = Array.Empty<DateTime>();
    public bool[] Given { get; set; } = Array.Empty<bool>();

}
