// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Interfaces;

namespace Domain.Entities;

public class AuditEntry : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Metadata { get; set; } = string.Empty;
    public DateTime StartTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }
    public bool Succeeded { get; set; }
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;

}
