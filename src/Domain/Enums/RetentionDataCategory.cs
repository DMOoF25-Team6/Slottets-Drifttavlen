// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Domain.Enums;

/// <summary>
/// Categories of personal data managed by GDPR retention policies (UC-010).
/// </summary>
public enum RetentionDataCategory
{
    MedicineLogs,
    ResidentNotes,
    AuditLogs,
    LoginLogs,
    InactiveUsers,
    AnonymizationTrigger
}
