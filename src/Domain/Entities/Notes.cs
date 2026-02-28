// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Interfaces;

namespace Domain.Entities;

public class ResidentNotes : IEntity
{
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Note { get; set; } = string.Empty;
    public Guid CareTakers { get; set; }
}

