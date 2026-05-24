// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Json;

using Core.DTOs.Sar;

using Domain.Entities;
using Domain.Enums;

namespace WebApi.Tests.Controllers.SubjectAccessRequest;

/// <summary>
/// Integration tests for SubjectAccessRequestController (UC-010 GDPR Article 15).
/// </summary>
public class SubjectAccessRequestControllerTests(CustomWebApplicationFactory<Api.Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly CustomWebApplicationFactory<Api.Program> _factory = factory;
    private readonly HttpClient _client = factory.CreateAuthenticatedClient();

    [Fact]
    public async Task GenerateExport_EmptyResidentId_ReturnsBadRequest()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            "/subjectaccessrequest/export", new SarExportRequestDto { ResidentId = Guid.Empty }, ct);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GenerateExport_UnknownResident_ReturnsNotFound()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            "/subjectaccessrequest/export", new SarExportRequestDto { ResidentId = Guid.NewGuid() }, ct);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GenerateExport_WithSeededResident_ReturnsOk()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        Guid residentId = Guid.NewGuid();
        _factory.Seed(db => db.Residents.Add(new Resident
        {
            Id = residentId,
            Initials = "SR",
            TrafficLightStatus = TrafficLightStatus.Green,
            Department = Department.Slottet
        }));

        HttpResponseMessage response = await _client.PostAsJsonAsync(
            "/subjectaccessrequest/export",
            new SarExportRequestDto { ResidentId = residentId, ScopeOptions = ["notes", "medicine"] }, ct);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task MarkFulfilled_EmptySarId_ReturnsBadRequest()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            "/subjectaccessrequest/fulfilled", new SarFulfilledDto { SarId = Guid.Empty }, ct);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task MarkFulfilled_UnknownSar_ReturnsNotFound()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            "/subjectaccessrequest/fulfilled",
            new SarFulfilledDto { SarId = Guid.NewGuid(), FulfilledAt = DateTime.UtcNow }, ct);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task MarkFulfilled_WithSeededSar_ReturnsOk()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        Guid sarId = Guid.NewGuid();
        _factory.Seed(db => db.SubjectAccessRequests.Add(new Domain.Entities.SubjectAccessRequest
        {
            Id = sarId,
            ResidentId = Guid.NewGuid(),
            RequestedByEmployeeId = Guid.NewGuid(),
            RequestedAt = DateTime.UtcNow,
            ExportFileName = "f.json",
            ExportGeneratedAt = DateTime.UtcNow
        }));

        HttpResponseMessage response = await _client.PostAsJsonAsync(
            "/subjectaccessrequest/fulfilled",
            new SarFulfilledDto { SarId = sarId, FulfilledAt = DateTime.UtcNow }, ct);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
