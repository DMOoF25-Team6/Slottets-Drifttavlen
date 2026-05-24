// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;

using Core.DTOs;

using Infrastructure.Managers;

using Moq;

namespace Infrastructure.Tests.Managers;

public class MedicineDeliveryManagerTests : HttpManagerTestBase
{
    private readonly MedicineDeliveryManager _manager;

    public MedicineDeliveryManagerTests() => _manager = new MedicineDeliveryManager(FactoryMock.Object);

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetAllAsync_DeserialisesPayload()
    {
        MedicineDeliveryResponseDto[] payload = [new() { Id = Guid.NewGuid(), MedicineName = "Panodil" }];
        Setup("medicinedelivery", Json(HttpStatusCode.OK, payload));

        IEnumerable<MedicineDeliveryResponseDto> result = await _manager.GetAllAsync(CancellationToken.None);

        _ = Assert.Single(result);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetByIdAsync_WhenOk_ReturnsDto()
    {
        Guid id = Guid.NewGuid();
        Setup($"medicinedelivery/{id}", Json(HttpStatusCode.OK, new MedicineDeliveryResponseDto { Id = id }));

        MedicineDeliveryResponseDto? result = await _manager.GetByIdAsync(id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(id, result!.Id);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetByIdAsync_When404_ReturnsNull()
    {
        Guid id = Guid.NewGuid();
        Setup($"medicinedelivery/{id}", Status(HttpStatusCode.NotFound));

        MedicineDeliveryResponseDto? result = await _manager.GetByIdAsync(id, CancellationToken.None);

        Assert.Null(result);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task CreateAsync_PostsToEndpoint()
    {
        Setup("medicinedelivery", Status(HttpStatusCode.Created));

        await _manager.CreateAsync(new MedicineDeliveryCreateRequestDto { MedicineName = "X" }, CancellationToken.None);

        VerifySent("medicinedelivery", Times.Once());
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task CreateAsync_WhenError_Throws()
    {
        Setup("medicinedelivery", Status(HttpStatusCode.BadRequest));

        _ = await Assert.ThrowsAsync<HttpRequestException>(
            () => _manager.CreateAsync(new MedicineDeliveryCreateRequestDto(), CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task UpdateAsync_PutsToEndpoint()
    {
        Guid id = Guid.NewGuid();
        Setup($"medicinedelivery/{id}", Status(HttpStatusCode.NoContent));

        await _manager.UpdateAsync(id, new MedicineDeliveryUpdateRequestDto { MedicineName = "Y" }, CancellationToken.None);

        VerifySent($"medicinedelivery/{id}", Times.Once());
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task UpdateAsync_WhenError_Throws()
    {
        Guid id = Guid.NewGuid();
        Setup($"medicinedelivery/{id}", Status(HttpStatusCode.NotFound));

        _ = await Assert.ThrowsAsync<HttpRequestException>(
            () => _manager.UpdateAsync(id, new MedicineDeliveryUpdateRequestDto(), CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task DeleteAsync_DeletesEndpoint()
    {
        Guid id = Guid.NewGuid();
        Setup($"medicinedelivery/{id}", Status(HttpStatusCode.NoContent));

        await _manager.DeleteAsync(id, CancellationToken.None);

        VerifySent($"medicinedelivery/{id}", Times.Once());
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task DeleteAsync_WhenError_Throws()
    {
        Guid id = Guid.NewGuid();
        Setup($"medicinedelivery/{id}", Status(HttpStatusCode.InternalServerError));

        _ = await Assert.ThrowsAsync<HttpRequestException>(() => _manager.DeleteAsync(id, CancellationToken.None));
    }
}
