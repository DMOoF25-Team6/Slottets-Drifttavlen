// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Domain.Enums;

/// <summary>
/// Lifecycle status of an anonymization candidate awaiting Admin review (UC-010).
/// </summary>
public enum AnonymizationStatus
{
    Pending,
    Approved,
    Rejected,
    Postponed,
    OnHold,
    Completed
}
