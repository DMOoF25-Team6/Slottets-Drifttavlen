// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;

using Core.DTOs.Retention;

using Infrastructure.Managers;

namespace Infrastructure.Tests.Managers;

public class RetentionPolicyManagerTests : HttpManagerTestBase
{
    private readonly RetentionPolicyManager _manager;

    public RetentionPolicyManagerTests() => _manager = new RetentionPolicyManager(FactoryMock.Object);

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetPoliciesAsync_DeserialisesPayload()
    {
        RetentionPolicyDto[] payload = [new(), new()];
        Setup("retentionpolicy", Json(HttpStatusCode.OK, payload));

        IEnumerable<RetentionPolicyDto> result = await _manager.GetPoliciesAsync(CancellationToken.None);

        Assert.Equal(2, result.Count());
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task UpdateRetentionPolicyAsync_PutsAndReturnsDto()
    {
        Setup("retentionpolicy", RawJson(HttpStatusCode.OK, "{}"));

        RetentionPolicyDto result = await _manager.UpdateRetentionPolicyAsync(
            new UpdateRetentionPolicyDto { Reason = "r", RetentionPeriod = TimeSpan.FromDays(30) },
            Guid.NewGuid(),
            CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task UpdateRetentionPolicyAsync_WhenError_Throws()
    {
        Setup("retentionpolicy", Status(HttpStatusCode.BadRequest));

        _ = await Assert.ThrowsAsync<HttpRequestException>(
            () => _manager.UpdateRetentionPolicyAsync(new UpdateRetentionPolicyDto(), Guid.NewGuid(), CancellationToken.None));
    }
}
