// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


using System.Net;
using System.Net.Http.Json;


using Core.DTOs;


namespace WebApi.Tests.Controllers.Medicine;

/// <summary>
/// Integration tests for MedicineController.
/// These tests verify that the API endpoints behave correctly when called over HTTP.
/// </summary>

public class MedicineControllerTests : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client;

    /// <summary>
    /// Creates an HttpClient using the test server provided by CustomWebApplicationFactory.
    /// This allows us to call the API as if it was running.
    /// </summary>

    public MedicineControllerTests(CustomWebApplicationFactory<Api.Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetMedicineStatus_ValidResidentId_ReturnsOk()
    {
        // Arrange: create a valid resident ID (GUID format expected by API)
        Guid residentId = Guid.NewGuid();

        // Act: call the endpoint using HttpClient
        HttpResponseMessage response = await _client.GetAsync($"/Medicine/{residentId}", TestContext.Current.CancellationToken);

        // Assert: check that the response status code is 200 OK
        _ = response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Fact]
    public async Task GetMedicineStatus_ValidResidentId_ReturnsMedicineStatusDto()
    {
        // Arrange: create a valid resident ID (GUID format expected by API)
        Guid residentId = Guid.NewGuid();

        // Act: call the endpoint. The API should return a MedicineStatusDto.
        MedicineStatusDto? result = await _client.GetFromJsonAsync<MedicineStatusDto>(
            $"/Medicine/{residentId}",
            TestContext.Current.CancellationToken);

        // Assert: confirm that a valid DTO is returned
        Assert.NotNull(result);
        Assert.Equal(residentId.ToString(), result.ResidentId);
    }


    [Fact]
    public async Task GetPainkillerStatus_ValidResidentId_ReturnsOk()
    {
        // Arrange: create a valid resident ID
        Guid residentId = Guid.NewGuid();

        // Act: call the painkiller endpoint
        HttpResponseMessage response = await _client.GetAsync($"/Medicine/painkiller/{residentId}", TestContext.Current.CancellationToken);

        // Assert: confirm the request succeeded (HTTP 200)
        _ = response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Fact]
    public async Task GetPainkillerStatus_ValidResidentId_ReturnsPainkillerStatusDto()
    {
        // Arrange: create a valid resident ID
        Guid residentId = Guid.NewGuid();

        // Act: call endpoint and expect a PainkillerStatusDto in response.
        PainkillerStatusDto? result = await _client.GetFromJsonAsync<PainkillerStatusDto>(
            $"/Medicine/painkiller/{residentId}",
            TestContext.Current.CancellationToken);

        // Assert: confirm response is not null
        Assert.NotNull(result);

        // confirm correct resident ID is returned
        Assert.Equal(residentId, result.ResidentId);
    }



    [Fact]
    public async Task GetMedicineStatus_InvalidResidentId_ReturnsBadRequest()
    {
        // Act: send an invalid GUID
        HttpResponseMessage response = await _client.GetAsync("/Medicine/not-a-guid", TestContext.Current.CancellationToken);

        // Assert: API should reject invalid input.
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetPainkillerStatus_InvalidResidentId_ReturnsBadRequest()
    {
        // Act: send invalid GUID to painkiller endpoint
        HttpResponseMessage response = await _client.GetAsync("/Medicine/painkiller/not-a-guid", TestContext.Current.CancellationToken);

        // Assert: API should reject invalid input.
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


}

