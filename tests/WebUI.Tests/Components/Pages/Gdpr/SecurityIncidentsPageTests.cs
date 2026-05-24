// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Bunit;
using Bunit.TestDoubles;

using Core.DTOs.Security;
using Core.Interfaces.Services;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using SecurityIncidentsPageComponent = WebUI.Client.Components.Pages.Gdpr.SecurityIncidentsPage;

namespace WebUI.Tests.Components.Pages.Gdpr;

/// <summary>
/// bUnit tests for the Security Incidents admin page (UC-011).
/// </summary>
public class SecurityIncidentsPageTests : Bunit.TestContext
{
    private readonly Mock<ISecurityIncidentService> _service = new();

    public SecurityIncidentsPageTests()
    {
        TestAuthorizationContext auth = this.AddTestAuthorization();
        auth.SetAuthorized("admin@example.com");
        auth.SetRoles("admin");

        _ = _service.Setup(s => s.GetIncidentsAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);

        _ = Services.AddScoped(_ => _service.Object);
    }

    [Fact]
    public void Render_AsAdmin_ShowsHeading()
    {
        IRenderedComponent<SecurityIncidentsPageComponent> cut = RenderComponent<SecurityIncidentsPageComponent>();

        cut.WaitForAssertion(() => Assert.Contains("Security Incidents", cut.Markup));
    }

    [Fact]
    public void Render_AsAdmin_LoadsIncidentsFromService()
    {
        IRenderedComponent<SecurityIncidentsPageComponent> cut = RenderComponent<SecurityIncidentsPageComponent>();

        cut.WaitForAssertion(() =>
            _service.Verify(s => s.GetIncidentsAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce));
    }

    [Fact]
    public void Render_WithIncident_ShowsIncidentType()
    {
        SecurityIncidentDto[] incidents = [new() { Id = Guid.NewGuid(), Type = "BruteForce" }];
        _ = _service.Setup(s => s.GetIncidentsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(incidents);

        IRenderedComponent<SecurityIncidentsPageComponent> cut = RenderComponent<SecurityIncidentsPageComponent>();

        cut.WaitForAssertion(() => Assert.Contains("BruteForce", cut.Markup));
    }
}
