// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Interfaces;

namespace Domain.Entities;

public class PainkillerRecord : IEntity
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey("Resident")]
    public Guid ResidentId { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime GivenAt { get; set; }
    // This property should not be here as it is a calculated value based on the type of painkiller and the time it was given. Tirsvad.
    public DateTime NextAllowedTime { get; set; }
}
