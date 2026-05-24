// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Services;

using Moq;

namespace Core.Tests.Services;

public class MedicineStatusServiceTests
{
    private readonly Mock<IMedicineStatusManager> _manager = new();
    private readonly MedicineStatusService _service;

    public MedicineStatusServiceTests() => _service = new MedicineStatusService(_manager.Object);

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetMedicineStatusAsync_DelegatesWithResidentId()
    {
        Guid residentId = Guid.NewGuid();
        _ = _manager.Setup(m => m.GetMedicineStatusAsync(residentId, It.IsAny<CancellationToken>())).ReturnsAsync(new MedicineStatusDto());

        MedicineStatusDto? result = await _service.GetMedicineStatusAsync(residentId, CancellationToken.None);

        Assert.NotNull(result);
        _manager.Verify(m => m.GetMedicineStatusAsync(residentId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetPainkillerStatusAsync_DelegatesWithResidentId()
    {
        Guid residentId = Guid.NewGuid();
        _ = _manager.Setup(m => m.GetPainkillerStatusAsync(residentId, It.IsAny<CancellationToken>())).ReturnsAsync(new PainkillerStatusDto());

        PainkillerStatusDto? result = await _service.GetPainkillerStatusAsync(residentId, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetMedicineStatusAsync_WhenManagerReturnsNull_ReturnsNull()
    {
        _ = _manager.Setup(m => m.GetMedicineStatusAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((MedicineStatusDto?)null);

        MedicineStatusDto? result = await _service.GetMedicineStatusAsync(Guid.NewGuid(), CancellationToken.None);

        Assert.Null(result);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetPainkillerStatusAsync_WhenManagerThrows_Propagates()
    {
        _ = _manager.Setup(m => m.GetPainkillerStatusAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ThrowsAsync(new HttpRequestException());

        _ = await Assert.ThrowsAsync<HttpRequestException>(() => _service.GetPainkillerStatusAsync(Guid.NewGuid(), CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Concurrency")]
    public async Task GetMedicineStatusAsync_ManyParallelCalls_AllSucceed()
    {
        _ = _manager.Setup(m => m.GetMedicineStatusAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(new MedicineStatusDto());

        MedicineStatusDto?[] results = await Task.WhenAll(
            Enumerable.Range(0, 100).Select(_ => _service.GetMedicineStatusAsync(Guid.NewGuid(), CancellationToken.None)));

        Assert.All(results, Assert.NotNull);
        _manager.Verify(m => m.GetMedicineStatusAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Exactly(100));
    }
}
