// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Domain.Enums;

/// <summary>
/// Lifecycle status of a security incident (UC-010).
/// BreachNotified indicates that the GDPR Article 33 notification has been sent to the DPO.
/// </summary>
public enum IncidentStatus
{
    Open,
    UnderInvestigation,
    Closed,
    Escalated,
    BreachNotified
}
