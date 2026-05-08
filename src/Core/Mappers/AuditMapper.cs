// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Audit;

using Domain.Entities;

namespace Core.Mappers;

/// <summary>
/// Provides mapping methods between audit-related domain entities and their data transfer objects.
/// </summary>
/// <remarks>
/// Centralises entity-to-DTO conversion for <see cref="AuditEntry"/> and <see cref="ChangeDetail"/>,
/// ensuring domain entities are never exposed across layer boundaries. Parses the
/// <see cref="AuditEntry.Metadata"/> string (formatted as "EntityName - ChangeType") into the
/// separate <see cref="AuditEntryDto.Entity"/> and <see cref="AuditEntryDto.ChangeType"/> fields
/// expected by UC-009 (View History and Traceability).
/// </remarks>
public static class AuditMapper
{
    private const string MetadataSeparator = " - ";

    #region AuditEntry mapping

    /// <summary>
    /// Maps an <see cref="AuditEntry"/> entity to an <see cref="AuditEntryDto"/>, optionally including the username.
    /// </summary>
    /// <param name="entity">The audit entry to map.</param>
    /// <param name="userName">The username of the user who performed the change, when known.</param>
    /// <returns>An <see cref="AuditEntryDto"/> representing the entity.</returns>
    public static AuditEntryDto ToDto(AuditEntry entity, string userName = "")
    {
        (string entityName, string changeType) = ParseMetadata(entity.Metadata);

        return new AuditEntryDto
        {
            Id = entity.Id,
            EventTimeUtc = entity.StartTimeUtc,
            RegisteredTimeUtc = entity.EndTimeUtc,
            Entity = entityName,
            ChangeType = changeType,
            Description = entity.Metadata,
            UserId = entity.UserId,
            UserName = userName,
            Succeeded = entity.Succeeded,
            ChangeDetails = ChangeDetailMapper.ToDtos(entity.ChangeDetails)
        };
    }

    /// <summary>
    /// Maps a collection of <see cref="AuditEntry"/> entities to a collection of <see cref="AuditEntryDto"/> objects.
    /// </summary>
    /// <param name="entities">The audit entries to map.</param>
    /// <returns>A collection of <see cref="AuditEntryDto"/> objects without usernames.</returns>
    public static IEnumerable<AuditEntryDto> ToDtos(IEnumerable<AuditEntry> entities)
    {
        return entities.Select(e => ToDto(e));
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Parses metadata of the form "EntityName - ChangeType" into its two components.
    /// </summary>
    /// <param name="metadata">The metadata string written by the audit interceptor.</param>
    /// <returns>A tuple containing the entity name and the change type. Empty strings are returned when parsing fails.</returns>
    private static (string EntityName, string ChangeType) ParseMetadata(string metadata)
    {
        if (string.IsNullOrWhiteSpace(metadata))
        {
            return (string.Empty, string.Empty);
        }

        int separatorIndex = metadata.IndexOf(MetadataSeparator, StringComparison.Ordinal);
        if (separatorIndex < 0)
        {
            return (metadata, string.Empty);
        }

        string entityName = metadata[..separatorIndex].Trim();
        string changeType = metadata[(separatorIndex + MetadataSeparator.Length)..].Trim();
        return (entityName, changeType);
    }

    #endregion
}
