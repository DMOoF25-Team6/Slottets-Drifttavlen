// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Bunit;
using Bunit.TestDoubles;

using Core.DTOs.Anonymization;
using Core.Interfaces.Services;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using GdprDashboard = WebUI.Client.Components.Pages.Gdpr.GdprDashboardPage;

namespace WebUI.Tests.Components.Pages.Gdpr;

/// <summary>
/// bUnit tests for the GDPR dashboard landing page (UC-010).
/// </summary>
public class GdprDashboardPageTests : Bunit.TestContext
{
    private readonly Mock<IAnonymizationService> _anonymization = new();
    private readonly Mock<ISecurityIncidentService> _incidents = new();

    public GdprDashboardPageTests()
    {
        TestAuthorizationContext auth = this.AddTestAuthorization();
        auth.SetAuthorized("admin@example.com");
        auth.SetRoles("admin");

        _ = _anonymization.Setup(s => s.GetCandidatesAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);
        _ = _incidents.Setup(s => s.GetIncidentsAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);

        _ = Services.AddScoped(_ => _anonymization.Object);
        _ = Services.AddScoped(_ => _incidents.Object);
    }

    [Fact]
    public void Render_AsAdmin_ShowsDashboardHeading()
    {
        IRenderedComponent<GdprDashboard> cut = RenderComponent<GdprDashboard>();

        cut.WaitForAssertion(() => Assert.Contains("GDPR Compliance Dashboard", cut.Markup));
    }

    [Fact]
    public void Render_AsAdmin_ShowsAllCards()
    {
        IRenderedComponent<GdprDashboard> cut = RenderComponent<GdprDashboard>();

        cut.WaitForAssertion(() =>
        {
            Assert.Contains("Retention Settings", cut.Markup);
            Assert.Contains("Anonymization Queue", cut.Markup);
            Assert.Contains("Security Incidents", cut.Markup);
            Assert.Contains("Subject Access Request", cut.Markup);
        });
    }

    [Fact]
    public void Render_WithPendingCandidates_LoadsCountersFromService()
    {
        AnonymizationCandidateDto[] candidates =
        [
            new() { Status = Domain.Enums.AnonymizationStatus.Pending },
            new() { Status = Domain.Enums.AnonymizationStatus.Pending }
        ];
        _ = _anonymization.Setup(s => s.GetCandidatesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(candidates);

        IRenderedComponent<GdprDashboard> cut = RenderComponent<GdprDashboard>();

        cut.WaitForAssertion(() =>
            _anonymization.Verify(s => s.GetCandidatesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce));
    }
}
