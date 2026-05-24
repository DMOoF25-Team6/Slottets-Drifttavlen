// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Retention;
using Core.Mappers;

using Domain.Entities;
using Domain.Enums;

namespace Core.Tests.Mappers;

public class RetentionPolicyMapperTests
{
    [Fact]
    [Trait("Category", "Functionality")]
    public void ToDto_MapsFields()
    {
        RetentionPolicy entity = new()
        {
            Id = Guid.NewGuid(),
            Category = RetentionDataCategory.MedicineLogs,
            RetentionPeriod = TimeSpan.FromDays(365),
            LegalMinimum = TimeSpan.FromDays(30),
            EffectiveFrom = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        };

        RetentionPolicyDto dto = RetentionPolicyMapper.ToDto(entity);

        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(RetentionDataCategory.MedicineLogs, dto.Category);
        Assert.Equal(TimeSpan.FromDays(365), dto.RetentionPeriod);
        Assert.Equal(TimeSpan.FromDays(30), dto.LegalMinimum);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public void ToAuditDto_MapsFields()
    {
        RetentionPolicyAudit entity = new()
        {
            Id = Guid.NewGuid(),
            RetentionPolicyId = Guid.NewGuid(),
            ChangedByEmployeeId = Guid.NewGuid(),
            PreviousPeriod = TimeSpan.FromDays(30),
            NewPeriod = TimeSpan.FromDays(60),
            ChangedAt = DateTime.UtcNow,
            Reason = "review"
        };

        RetentionPolicyAuditDto dto = RetentionPolicyMapper.ToAuditDto(entity);

        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(TimeSpan.FromDays(30), dto.PreviousPeriod);
        Assert.Equal(TimeSpan.FromDays(60), dto.NewPeriod);
        Assert.Equal("review", dto.Reason);
    }

    [Theory]
    [Trait("Category", "EdgeCase")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3650)]
    public void ToDto_AnyRetentionPeriod_RoundTrips(int days)
    {
        RetentionPolicy entity = new() { RetentionPeriod = TimeSpan.FromDays(days), Category = RetentionDataCategory.AuditLogs };

        RetentionPolicyDto dto = RetentionPolicyMapper.ToDto(entity);

        Assert.Equal(TimeSpan.FromDays(days), dto.RetentionPeriod);
    }

    [Fact]
    [Trait("Category", "Concurrency")]
    public void ToDto_ParallelMapping_IsThreadSafe()
    {
        RetentionPolicy entity = new() { Id = Guid.NewGuid(), Category = RetentionDataCategory.LoginLogs };

        RetentionPolicyDto[] results = Enumerable.Range(0, 200).AsParallel().Select(_ => RetentionPolicyMapper.ToDto(entity)).ToArray();

        Assert.All(results, r => Assert.Equal(entity.Id, r.Id));
    }
}
