// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Security;
using Core.Mappers;

using Domain.Entities;

namespace Core.Tests.Mappers;

public class SecurityIncidentMapperTests
{
    [Fact]
    [Trait("Category", "Functionality")]
    public void ToDto_MapsFields()
    {
        SecurityIncident entity = new()
        {
            Id = Guid.NewGuid(),
            DetectedAt = new DateTime(2026, 5, 1, 8, 0, 0, DateTimeKind.Utc),
            Type = "BruteForce",
            InvestigationNotes = "looking"
        };

        SecurityIncidentDto dto = SecurityIncidentMapper.ToDto(entity);

        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal("BruteForce", dto.Type);
        Assert.Equal("looking", dto.InvestigationNotes);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void ToDto_DeadlineIs72HoursAfterDetection()
    {
        DateTime detected = new(2026, 5, 1, 8, 0, 0, DateTimeKind.Utc);
        SecurityIncident entity = new() { DetectedAt = detected, Type = "X" };

        SecurityIncidentDto dto = SecurityIncidentMapper.ToDto(entity);

        Assert.Equal(detected.AddHours(72), dto.BreachNotificationDeadlineUtc);
    }

    [Fact]
    [Trait("Category", "Concurrency")]
    public void ToDto_ParallelMapping_IsThreadSafe()
    {
        SecurityIncident entity = new() { Id = Guid.NewGuid(), DetectedAt = DateTime.UtcNow, Type = "X" };

        SecurityIncidentDto[] results = Enumerable.Range(0, 200).AsParallel().Select(_ => SecurityIncidentMapper.ToDto(entity)).ToArray();

        Assert.All(results, r => Assert.Equal(entity.Id, r.Id));
    }
}
