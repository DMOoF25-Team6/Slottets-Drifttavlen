// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;

using Core.DTOs;

using Infrastructure.Managers;

namespace Infrastructure.Tests.Managers;

public class MedicineStatusManagerTests : HttpManagerTestBase
{
    private readonly MedicineStatusManager _manager;

    public MedicineStatusManagerTests() => _manager = new MedicineStatusManager(FactoryMock.Object);

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetMedicineStatusAsync_ReturnsDto()
    {
        Guid residentId = Guid.NewGuid();
        Setup(residentId.ToString(), Json(HttpStatusCode.OK, new MedicineStatusDto()));

        MedicineStatusDto? result = await _manager.GetMedicineStatusAsync(residentId, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetPainkillerStatusAsync_ReturnsDto()
    {
        Guid residentId = Guid.NewGuid();
        Setup(residentId.ToString(), Json(HttpStatusCode.OK, new PainkillerStatusDto()));

        PainkillerStatusDto? result = await _manager.GetPainkillerStatusAsync(residentId, CancellationToken.None);

        Assert.NotNull(result);
    }
}
