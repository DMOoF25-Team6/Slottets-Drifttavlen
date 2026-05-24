// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;

using Core.DTOs.Security;

using Infrastructure.Managers;

namespace Infrastructure.Tests.Managers;

public class SecurityIncidentManagerTests : HttpManagerTestBase
{
    private readonly SecurityIncidentManager _manager;

    public SecurityIncidentManagerTests() => _manager = new SecurityIncidentManager(FactoryMock.Object);

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetIncidentsAsync_DeserialisesPayload()
    {
        SecurityIncidentDto[] payload = [new(), new()];
        Setup("securityincident", Json(HttpStatusCode.OK, payload));

        IEnumerable<SecurityIncidentDto> result = await _manager.GetIncidentsAsync(CancellationToken.None);

        Assert.Equal(2, result.Count());
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task EscalateIncidentAsync_PostsAndReturnsDto()
    {
        Guid id = Guid.NewGuid();
        Setup($"securityincident/{id}/escalate", RawJson(HttpStatusCode.OK, "{}"));

        SecurityIncidentDto result = await _manager.EscalateIncidentAsync(id, true, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task EscalateIncidentAsync_WhenError_Throws()
    {
        Guid id = Guid.NewGuid();
        Setup($"securityincident/{id}/escalate", Status(HttpStatusCode.BadRequest));

        _ = await Assert.ThrowsAsync<HttpRequestException>(
            () => _manager.EscalateIncidentAsync(id, false, CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task AddInvestigationNotesAsync_PostsAndReturnsDto()
    {
        Guid id = Guid.NewGuid();
        Setup($"securityincident/{id}/notes", RawJson(HttpStatusCode.OK, "{}"));

        SecurityIncidentDto result = await _manager.AddInvestigationNotesAsync(
            new AddInvestigationNotesDto { IncidentId = id, Notes = "n" }, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task CloseIncidentAsync_PostsAndReturnsDto()
    {
        Guid id = Guid.NewGuid();
        Setup($"securityincident/{id}/close", RawJson(HttpStatusCode.OK, "{}"));

        SecurityIncidentDto result = await _manager.CloseIncidentAsync(id, CancellationToken.None);

        Assert.NotNull(result);
    }
}
