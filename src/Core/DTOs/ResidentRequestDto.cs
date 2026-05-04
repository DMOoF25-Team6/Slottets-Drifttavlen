// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Dto;

using Domain.Enums;

namespace Core.DTOs;

public class ResidentRequest : IResidentResult
{
    public required Guid Id { get; set; }
    public required string Initials { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required TrafficLightStatus? TrafficLightStatus { get; set; }
}
