// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Json;

using Core.DTOs;

namespace WebApi.Tests.Controllers.MedicineDelivery;

/// <summary>
/// Integration tests for MedicineDeliveryController (UC-020/021/022).
/// Exercises the full admin CRUD surface over HTTP against the in-memory test database.
/// </summary>
public class MedicineDeliveryControllerTests(CustomWebApplicationFactory<Api.Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client = factory.CreateAuthenticatedClient();

    private static MedicineDeliveryCreateRequestDto NewCreateDto() => new()
    {
        ResidentId = Guid.NewGuid(),
        MedicineName = "Panodil",
        Timestamp = DateTime.UtcNow,
        Given = true
    };

    private async Task<MedicineDeliveryResponseDto> CreateAsync(CancellationToken ct)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/medicinedelivery", NewCreateDto(), ct);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        MedicineDeliveryResponseDto? created = await response.Content.ReadFromJsonAsync<MedicineDeliveryResponseDto>(cancellationToken: ct);
        Assert.NotNull(created);
        return created!;
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        HttpResponseMessage response = await _client.GetAsync("/medicinedelivery", ct);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Create_ReturnsCreated_WithGeneratedId()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        MedicineDeliveryResponseDto created = await CreateAsync(ct);

        Assert.NotEqual(Guid.Empty, created.Id);
        Assert.Equal("Panodil", created.MedicineName);
        Assert.True(created.Given);
    }

    [Fact]
    public async Task GetById_AfterCreate_ReturnsOk()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        MedicineDeliveryResponseDto created = await CreateAsync(ct);

        MedicineDeliveryResponseDto? fetched =
            await _client.GetFromJsonAsync<MedicineDeliveryResponseDto>($"/medicinedelivery/{created.Id}", ct);

        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched!.Id);
    }

    [Fact]
    public async Task GetById_UnknownId_ReturnsNotFound()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        HttpResponseMessage response = await _client.GetAsync($"/medicinedelivery/{Guid.NewGuid()}", ct);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_AfterCreate_ContainsCreatedItem()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        MedicineDeliveryResponseDto created = await CreateAsync(ct);

        IEnumerable<MedicineDeliveryResponseDto>? all =
            await _client.GetFromJsonAsync<IEnumerable<MedicineDeliveryResponseDto>>("/medicinedelivery", ct);

        Assert.NotNull(all);
        Assert.Contains(all!, d => d.Id == created.Id);
    }

    [Fact]
    public async Task Update_AfterCreate_ReturnsNoContent_AndPersists()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        MedicineDeliveryResponseDto created = await CreateAsync(ct);

        MedicineDeliveryUpdateRequestDto update = new()
        {
            ResidentId = created.ResidentId,
            MedicineName = "Ipren",
            Timestamp = created.Timestamp,
            Given = false
        };

        HttpResponseMessage response = await _client.PutAsJsonAsync($"/medicinedelivery/{created.Id}", update, ct);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        MedicineDeliveryResponseDto? fetched =
            await _client.GetFromJsonAsync<MedicineDeliveryResponseDto>($"/medicinedelivery/{created.Id}", ct);
        Assert.NotNull(fetched);
        Assert.Equal("Ipren", fetched!.MedicineName);
        Assert.False(fetched.Given);
    }

    [Fact]
    public async Task Update_UnknownId_ReturnsNotFound()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        MedicineDeliveryUpdateRequestDto update = new()
        {
            ResidentId = Guid.NewGuid(),
            MedicineName = "X",
            Timestamp = DateTime.UtcNow,
            Given = false
        };

        HttpResponseMessage response = await _client.PutAsJsonAsync($"/medicinedelivery/{Guid.NewGuid()}", update, ct);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_AfterCreate_ReturnsNoContent_ThenGone()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        MedicineDeliveryResponseDto created = await CreateAsync(ct);

        HttpResponseMessage delete = await _client.DeleteAsync($"/medicinedelivery/{created.Id}", ct);
        Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);

        HttpResponseMessage getAfter = await _client.GetAsync($"/medicinedelivery/{created.Id}", ct);
        Assert.Equal(HttpStatusCode.NotFound, getAfter.StatusCode);
    }

    [Fact]
    public async Task Delete_UnknownId_ReturnsNotFound()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        HttpResponseMessage response = await _client.DeleteAsync($"/medicinedelivery/{Guid.NewGuid()}", ct);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
