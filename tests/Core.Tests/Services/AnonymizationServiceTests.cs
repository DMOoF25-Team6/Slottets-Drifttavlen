// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Anonymization;
using Core.Interfaces.Managers;
using Core.Services;

using Moq;

namespace Core.Tests.Services;

public class AnonymizationServiceTests
{
    private readonly Mock<IAnonymizationManager> _manager = new();
    private readonly AnonymizationService _service;

    public AnonymizationServiceTests() => _service = new AnonymizationService(_manager.Object);

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void Constructor_NullManager_Throws() =>
        Assert.Throws<ArgumentNullException>(() => new AnonymizationService(null!));

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetCandidatesAsync_DelegatesAndReturns()
    {
        AnonymizationCandidateDto[] expected = [new(), new()];
        _ = _manager.Setup(m => m.GetCandidatesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        IEnumerable<AnonymizationCandidateDto> result = await _service.GetCandidatesAsync(CancellationToken.None);

        Assert.Equal(2, result.Count());
        _manager.Verify(m => m.GetCandidatesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task ApproveAnonymizationAsync_DelegatesWithId()
    {
        Guid id = Guid.NewGuid();
        _ = _manager.Setup(m => m.ApproveAnonymizationAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(new AnonymizationResultDto());

        AnonymizationResultDto result = await _service.ApproveAnonymizationAsync(id, CancellationToken.None);

        Assert.NotNull(result);
        _manager.Verify(m => m.ApproveAnonymizationAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task RejectAnonymizationAsync_PassesReason()
    {
        Guid id = Guid.NewGuid();
        _ = _manager.Setup(m => m.RejectAnonymizationAsync(id, "dup", It.IsAny<CancellationToken>())).ReturnsAsync(true);

        bool ok = await _service.RejectAnonymizationAsync(id, "dup", CancellationToken.None);

        Assert.True(ok);
        _manager.Verify(m => m.RejectAnonymizationAsync(id, "dup", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetCandidatesAsync_WhenManagerThrows_Propagates()
    {
        _ = _manager.Setup(m => m.GetCandidatesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new HttpRequestException("down"));

        _ = await Assert.ThrowsAsync<HttpRequestException>(() => _service.GetCandidatesAsync(CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Concurrency")]
    public async Task GetCandidatesAsync_ManyParallelCalls_AllSucceed()
    {
        _ = _manager.Setup(m => m.GetCandidatesAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);

        IEnumerable<AnonymizationCandidateDto>[] results = await Task.WhenAll(
            Enumerable.Range(0, 100).Select(_ => _service.GetCandidatesAsync(CancellationToken.None)));

        Assert.All(results, Assert.NotNull);
        _manager.Verify(m => m.GetCandidatesAsync(It.IsAny<CancellationToken>()), Times.Exactly(100));
    }
}
