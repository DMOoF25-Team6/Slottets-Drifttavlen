// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.Interfaces.Services;

/// <summary>
/// Defines a contract for sending GDPR Article 33 breach notifications to the DPO (UC-010).
/// </summary>
/// <remarks>
/// Implementation lives in Infrastructure layer (SMTP-based notification).
/// </remarks>
public interface IArt33NotificationService
{
    Task<bool> SendNotificationAsync(Guid incidentId, string dpoEmail, CancellationToken cancellationToken);
}
