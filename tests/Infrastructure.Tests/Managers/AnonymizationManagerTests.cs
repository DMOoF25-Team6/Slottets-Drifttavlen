// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;

using Core.DTOs.Anonymization;

using Infrastructure.Managers;

namespace Infrastructure.Tests.Managers;

public class AnonymizationManagerTests : HttpManagerTestBase
{
    private readonly AnonymizationManager _manager;

    public AnonymizationManagerTests() => _manager = new AnonymizationManager(FactoryMock.Object);

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetCandidatesAsync_DeserialisesPayload()
    {
        AnonymizationCandidateDto[] payload = [new(), new(), new()];
        Setup("anonymization/candidates", Json(HttpStatusCode.OK, payload));

        IEnumerable<AnonymizationCandidateDto> result = await _manager.GetCandidatesAsync(CancellationToken.None);

        Assert.Equal(3, result.Count());
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task ApproveAnonymizationAsync_PostsAndReturnsResult()
    {
        Guid id = Guid.NewGuid();
        Setup($"anonymization/{id}/approve", RawJson(HttpStatusCode.OK, "{}"));

        AnonymizationResultDto result = await _manager.ApproveAnonymizationAsync(id, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task ApproveAnonymizationAsync_WhenError_Throws()
    {
        Guid id = Guid.NewGuid();
        Setup($"anonymization/{id}/approve", Status(HttpStatusCode.BadRequest));

        _ = await Assert.ThrowsAsync<HttpRequestException>(
            () => _manager.ApproveAnonymizationAsync(id, CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task RejectAnonymizationAsync_WhenOk_ReturnsTrue()
    {
        Guid id = Guid.NewGuid();
        Setup($"anonymization/{id}/reject", Status(HttpStatusCode.OK));

        bool ok = await _manager.RejectAnonymizationAsync(id, "reason", CancellationToken.None);

        Assert.True(ok);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task RejectAnonymizationAsync_WhenError_ReturnsFalse()
    {
        Guid id = Guid.NewGuid();
        Setup($"anonymization/{id}/reject", Status(HttpStatusCode.BadRequest));

        bool ok = await _manager.RejectAnonymizationAsync(id, "reason", CancellationToken.None);

        Assert.False(ok);
    }
}
