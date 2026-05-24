// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;

using Core.DTOs.Sar;

using Infrastructure.Managers;

namespace Infrastructure.Tests.Managers;

public class SubjectAccessRequestManagerTests : HttpManagerTestBase
{
    private readonly SubjectAccessRequestManager _manager;

    public SubjectAccessRequestManagerTests() => _manager = new SubjectAccessRequestManager(FactoryMock.Object);

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GenerateExportAsync_PostsAndReturnsPackage()
    {
        Setup("subjectaccessrequest/export", RawJson(HttpStatusCode.OK, "{}"));

        SarExportPackageDto result = await _manager.GenerateExportAsync(
            new SarExportRequestDto { ResidentId = Guid.NewGuid() }, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GenerateExportAsync_WhenError_Throws()
    {
        Setup("subjectaccessrequest/export", Status(HttpStatusCode.BadRequest));

        _ = await Assert.ThrowsAsync<HttpRequestException>(
            () => _manager.GenerateExportAsync(new SarExportRequestDto(), CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task MarkFulfilledAsync_WhenOk_ReturnsTrue()
    {
        Setup("subjectaccessrequest/fulfilled", Status(HttpStatusCode.OK));

        bool ok = await _manager.MarkFulfilledAsync(
            new SarFulfilledDto { SarId = Guid.NewGuid(), FulfilledAt = DateTime.UtcNow }, CancellationToken.None);

        Assert.True(ok);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task MarkFulfilledAsync_WhenError_ReturnsFalse()
    {
        Setup("subjectaccessrequest/fulfilled", Status(HttpStatusCode.BadRequest));

        bool ok = await _manager.MarkFulfilledAsync(new SarFulfilledDto(), CancellationToken.None);

        Assert.False(ok);
    }
}
