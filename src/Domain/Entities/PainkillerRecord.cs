// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Interfaces;

namespace Domain.Entities;

public class PainkillerRecord : IEntity
{
    public Guid Id { get; set; }
    public Guid ResidentId { get; set; }
    public virtual Resident Resident { get; set; } = null!;
    public string Type { get; set; } = string.Empty;
    public DateTime GivenAt { get; set; }
    public DateTime NextAllowedTime { get; set; }
}
