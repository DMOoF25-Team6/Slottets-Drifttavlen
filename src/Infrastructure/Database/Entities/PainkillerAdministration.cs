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

/// <summary>
/// Represents a painkiller administration in the database.
/// Each record tracks painkiller given to a resident,
/// and the time when the next dose is allowed.
/// </summary>

public class PainkillerAdministration
{
    // Primary key for the painkiller administration record
    [Key]
    public Guid Id { get; set; }

    // Foreign key referencing the resident.
    [Required]
    public Guid ResidentId { get; set; }

   
    // Navigation property to the related resident.
    [ForeignKey(nameof(ResidentId))]
    public virtual Resident Resident { get; set; } = null!;

    
    // What type of painkiller administered
    [Required]
    [MaxLength(100)]
    public string Type { get; set; } = string.Empty;

    
    /// Times when the painkiller was administered.
    [Required]
    public DateTime GivenAt { get; set; }

    
    // The next time this resident is allowed to receive this painkiller.
    [Required]
    public DateTime NextAllowedTime { get; set; }

}
