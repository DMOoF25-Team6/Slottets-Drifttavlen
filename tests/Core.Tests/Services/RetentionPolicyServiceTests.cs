// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Retention;
using Core.Interfaces.Managers;
using Core.Services;

using Moq;

namespace Core.Tests.Services;

public class RetentionPolicyServiceTests
{
    private readonly Mock<IRetentionPolicyManager> _manager = new();
    private readonly RetentionPolicyService _service;

    public RetentionPolicyServiceTests() => _service = new RetentionPolicyService(_manager.Object);

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void Constructor_NullManager_Throws() =>
        Assert.Throws<ArgumentNullException>(() => new RetentionPolicyService(null!));

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetPoliciesAsync_DelegatesAndReturns()
    {
        RetentionPolicyDto[] expected = [new(), new()];
        _ = _manager.Setup(m => m.GetPoliciesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        IEnumerable<RetentionPolicyDto> result = await _service.GetPoliciesAsync(CancellationToken.None);

        Assert.Equal(2, result.Count());
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task UpdateRetentionPolicyAsync_PassesEmployeeId()
    {
        Guid employeeId = Guid.NewGuid();
        UpdateRetentionPolicyDto dto = new() { Reason = "r", RetentionPeriod = TimeSpan.FromDays(30) };
        _ = _manager.Setup(m => m.UpdateRetentionPolicyAsync(dto, employeeId, It.IsAny<CancellationToken>())).ReturnsAsync(new RetentionPolicyDto());

        _ = await _service.UpdateRetentionPolicyAsync(dto, employeeId, CancellationToken.None);

        _manager.Verify(m => m.UpdateRetentionPolicyAsync(dto, employeeId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task UpdateRetentionPolicyAsync_WhenManagerThrows_Propagates()
    {
        _ = _manager.Setup(m => m.UpdateRetentionPolicyAsync(It.IsAny<UpdateRetentionPolicyDto>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException());

        _ = await Assert.ThrowsAsync<HttpRequestException>(
            () => _service.UpdateRetentionPolicyAsync(new UpdateRetentionPolicyDto(), Guid.NewGuid(), CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Concurrency")]
    public async Task GetPoliciesAsync_ManyParallelCalls_AllSucceed()
    {
        _ = _manager.Setup(m => m.GetPoliciesAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);

        IEnumerable<RetentionPolicyDto>[] results = await Task.WhenAll(
            Enumerable.Range(0, 100).Select(_ => _service.GetPoliciesAsync(CancellationToken.None)));

        Assert.All(results, Assert.NotNull);
        _manager.Verify(m => m.GetPoliciesAsync(It.IsAny<CancellationToken>()), Times.Exactly(100));
    }
}
