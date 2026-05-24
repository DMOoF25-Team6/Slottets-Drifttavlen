// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Sar;
using Core.Interfaces.Managers;
using Core.Services;

using Moq;

namespace Core.Tests.Services;

public class SubjectAccessRequestServiceTests
{
    private readonly Mock<ISubjectAccessRequestManager> _manager = new();
    private readonly SubjectAccessRequestService _service;

    public SubjectAccessRequestServiceTests() => _service = new SubjectAccessRequestService(_manager.Object);

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void Constructor_NullManager_Throws() =>
        Assert.Throws<ArgumentNullException>(() => new SubjectAccessRequestService(null!));

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GenerateExportAsync_DelegatesAndReturns()
    {
        SarExportRequestDto dto = new() { ResidentId = Guid.NewGuid() };
        _ = _manager.Setup(m => m.GenerateExportAsync(dto, It.IsAny<CancellationToken>())).ReturnsAsync(new SarExportPackageDto());

        SarExportPackageDto result = await _service.GenerateExportAsync(dto, CancellationToken.None);

        Assert.NotNull(result);
        _manager.Verify(m => m.GenerateExportAsync(dto, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task MarkFulfilledAsync_DelegatesAndReturns()
    {
        SarFulfilledDto dto = new() { SarId = Guid.NewGuid(), FulfilledAt = DateTime.UtcNow };
        _ = _manager.Setup(m => m.MarkFulfilledAsync(dto, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        bool ok = await _service.MarkFulfilledAsync(dto, CancellationToken.None);

        Assert.True(ok);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GenerateExportAsync_WhenManagerThrows_Propagates()
    {
        _ = _manager.Setup(m => m.GenerateExportAsync(It.IsAny<SarExportRequestDto>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException());

        _ = await Assert.ThrowsAsync<HttpRequestException>(
            () => _service.GenerateExportAsync(new SarExportRequestDto(), CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Concurrency")]
    public async Task MarkFulfilledAsync_ManyParallelCalls_AllDelegate()
    {
        _ = _manager.Setup(m => m.MarkFulfilledAsync(It.IsAny<SarFulfilledDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        bool[] results = await Task.WhenAll(
            Enumerable.Range(0, 100).Select(_ => _service.MarkFulfilledAsync(new SarFulfilledDto(), CancellationToken.None)));

        Assert.All(results, Assert.True);
        _manager.Verify(m => m.MarkFulfilledAsync(It.IsAny<SarFulfilledDto>(), It.IsAny<CancellationToken>()), Times.Exactly(100));
    }
}
