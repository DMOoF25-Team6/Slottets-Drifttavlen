// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class MedicineStatusView
{
    public Guid ResidentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ActiveMedicines { get; set; }

}
