// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Json;

using Core.DTOs.Retention;

using Domain.Entities;
using Domain.Enums;

namespace WebApi.Tests.Controllers.RetentionPolicy;

/// <summary>
/// Integration tests for RetentionPolicyController (UC-010 retention governance).
/// </summary>
public class RetentionPolicyControllerTests(CustomWebApplicationFactory<Api.Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly CustomWebApplicationFactory<Api.Program> _factory = factory;
    private readonly HttpClient _client = factory.CreateAuthenticatedClient();

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        HttpResponseMessage response = await _client.GetAsync("/retentionpolicy", ct);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Update_BelowLegalMinimum_ReturnsBadRequest()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        _factory.Seed(db => db.RetentionPolicies.Add(new Domain.Entities.RetentionPolicy
        {
            Id = Guid.NewGuid(),
            Category = RetentionDataCategory.LoginLogs,
            RetentionPeriod = TimeSpan.FromDays(365),
            LegalMinimum = TimeSpan.FromDays(100),
            EffectiveFrom = DateTime.UtcNow
        }));
        UpdateRetentionPolicyDto dto = new()
        {
            Category = RetentionDataCategory.LoginLogs,
            RetentionPeriod = TimeSpan.FromDays(1),
            Reason = "too short"
        };
        HttpResponseMessage response = await _client.PutAsJsonAsync(
            $"/retentionpolicy?changedByEmployeeId={Guid.NewGuid()}", dto, ct);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
