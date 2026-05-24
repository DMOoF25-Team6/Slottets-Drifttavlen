// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Json;

using Core.DTOs.Security;

using Domain.Entities;

namespace WebApi.Tests.Controllers.SecurityIncident;

/// <summary>
/// Integration tests for SecurityIncidentController (UC-011 security monitoring).
/// </summary>
public class SecurityIncidentControllerTests(CustomWebApplicationFactory<Api.Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly CustomWebApplicationFactory<Api.Program> _factory = factory;
    private readonly HttpClient _client = factory.CreateAuthenticatedClient();

    private Guid SeedIncident()
    {
        Guid id = Guid.NewGuid();
        _factory.Seed(db => db.SecurityIncidents.Add(new Domain.Entities.SecurityIncident
        {
            Id = id,
            DetectedAt = DateTime.UtcNow,
            Type = "BruteForce"
        }));
        return id;
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        HttpResponseMessage response = await _client.GetAsync("/securityincident", ct);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Escalate_UnknownId_ReturnsNotFound()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            $"/securityincident/{Guid.NewGuid()}/escalate", new EscalateIncidentDto { IsBreach = true }, ct);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Escalate_Seeded_ReturnsOk()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        Guid id = SeedIncident();
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            $"/securityincident/{id}/escalate", new EscalateIncidentDto { IncidentId = id, IsBreach = true }, ct);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task AddNotes_EmptyNotes_ReturnsBadRequest()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        Guid id = Guid.NewGuid();
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            $"/securityincident/{id}/notes", new AddInvestigationNotesDto { IncidentId = id, Notes = "" }, ct);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task AddNotes_Seeded_ReturnsOk()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        Guid id = SeedIncident();
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            $"/securityincident/{id}/notes", new AddInvestigationNotesDto { IncidentId = id, Notes = "looked into it" }, ct);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Close_UnknownId_ReturnsNotFound()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            $"/securityincident/{Guid.NewGuid()}/close", new { }, ct);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Close_Seeded_ReturnsOk()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        Guid id = SeedIncident();
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            $"/securityincident/{id}/close", new { }, ct);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
