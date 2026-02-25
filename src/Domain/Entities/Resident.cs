// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities;

public class Resident : IEntity
{
    public Guid Id { get; set; }
    public string Initials { get; set; } = string.Empty;
    public MentalStatus? MentalStatus { get; set; }
}
