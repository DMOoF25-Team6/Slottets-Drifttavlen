// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Security;

/// <summary>
/// Request payload for escalating a security incident (UC-010).
/// Setting IsBreach=true triggers GDPR Art. 33 notification to the DPO.
/// </summary>
public class EscalateIncidentDto
{
    public Guid IncidentId { get; set; }
    public bool IsBreach { get; set; }
}
