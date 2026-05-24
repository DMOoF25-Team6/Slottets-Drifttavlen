// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Security;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;
using Core.Services;

using Moq;

namespace Core.Tests.Services;

public class SecurityIncidentServiceTests
{
    private readonly Mock<ISecurityIncidentManager> _manager = new();
    private readonly Mock<IArt33NotificationService> _art33 = new();
    private readonly SecurityIncidentService _service;

    public SecurityIncidentServiceTests() => _service = new SecurityIncidentService(_manager.Object, _art33.Object);

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void Constructor_NullManager_Throws() =>
        Assert.Throws<ArgumentNullException>(() => new SecurityIncidentService(null!, _art33.Object));

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void Constructor_NullArt33_Throws() =>
        Assert.Throws<ArgumentNullException>(() => new SecurityIncidentService(_manager.Object, null!));

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetIncidentsAsync_DelegatesAndReturns()
    {
        SecurityIncidentDto[] expected = [new(), new(), new()];
        _ = _manager.Setup(m => m.GetIncidentsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        IEnumerable<SecurityIncidentDto> result = await _service.GetIncidentsAsync(CancellationToken.None);

        Assert.Equal(3, result.Count());
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task EscalateIncidentAsync_PassesBreachFlag()
    {
        Guid id = Guid.NewGuid();
        _ = _manager.Setup(m => m.EscalateIncidentAsync(id, true, It.IsAny<CancellationToken>())).ReturnsAsync(new SecurityIncidentDto());

        _ = await _service.EscalateIncidentAsync(id, true, CancellationToken.None);

        _manager.Verify(m => m.EscalateIncidentAsync(id, true, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task CloseIncidentAsync_DelegatesWithId()
    {
        Guid id = Guid.NewGuid();
        _ = _manager.Setup(m => m.CloseIncidentAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(new SecurityIncidentDto());

        _ = await _service.CloseIncidentAsync(id, CancellationToken.None);

        _manager.Verify(m => m.CloseIncidentAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task AddInvestigationNotesAsync_WhenManagerThrows_Propagates()
    {
        _ = _manager.Setup(m => m.AddInvestigationNotesAsync(It.IsAny<AddInvestigationNotesDto>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException());

        _ = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.AddInvestigationNotesAsync(new AddInvestigationNotesDto(), CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Concurrency")]
    public async Task EscalateIncidentAsync_ManyParallelCalls_AllDelegate()
    {
        _ = _manager.Setup(m => m.EscalateIncidentAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SecurityIncidentDto());

        SecurityIncidentDto[] results = await Task.WhenAll(
            Enumerable.Range(0, 100).Select(_ => _service.EscalateIncidentAsync(Guid.NewGuid(), false, CancellationToken.None)));

        Assert.All(results, Assert.NotNull);
        _manager.Verify(m => m.EscalateIncidentAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Exactly(100));
    }
}
