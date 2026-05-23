// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;

namespace WebApi.Tests.Controllers.Anonymization;

/// <summary>
/// Integration tests for AnonymizationController (UC-010 GDPR).
/// </summary>
public class AnonymizationControllerTests(CustomWebApplicationFactory<Api.Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client = factory.CreateAuthenticatedClient();

    [Fact]
    public async Task GetCandidates_AsAdmin_ReturnsOk()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        HttpResponseMessage response = await _client.GetAsync("/anonymization/candidates", ct);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
