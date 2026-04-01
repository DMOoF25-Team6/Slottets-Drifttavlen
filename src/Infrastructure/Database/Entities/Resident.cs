// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Database.Entities;

public class Resident
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(2)]
    public string Initials { get; set; } = string.Empty;

    // Store enum as int. TrafficLight is stored as an integer in the database to represent enum values.
    public int? TrafficLight { get; set; }


    // Navigation properties
    /// <summary>
    /// Collection of medicine and painkillers administrations for this resident.
    /// One resident can have many medicine records.
    /// </summary>

    public virtual ICollection<MedicineAdministration> Medicines { get; set; } = new List<MedicineAdministration>();

    public virtual ICollection<PainkillerAdministration> Painkillers { get; set; } = new List<PainkillerAdministration>();


}
