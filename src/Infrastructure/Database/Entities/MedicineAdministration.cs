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
/// Represents a medicine administration in the database.
/// Each record indicates whether a specific medicine was given
/// to a resident at a specific time.
/// </summary>

public class MedicineAdministration
{
    // Primary key medicine administration record
    [Key]
    public Guid Id { get; set; }


    // Foreign key to the resident who received the medicine
    [Required]
    public Guid ResidentId { get; set; }


    // Navigation property to the related resident entity
    [ForeignKey(nameof(ResidentId))]
    public virtual Resident Resident { get; set; } = null!;

  
    // Name of the medicine administered.
    [Required]
    [MaxLength(100)]
    public string MedicineName { get; set; } = string.Empty;

    
    //Timestamp indicating when the medicine should be or was given.
    [Required]
    public DateTime Timestamp { get; set; }

   
    // Indicates whether the medicine was given (true) or not (false).
    [Required]
    public bool Given { get; set; }

}
