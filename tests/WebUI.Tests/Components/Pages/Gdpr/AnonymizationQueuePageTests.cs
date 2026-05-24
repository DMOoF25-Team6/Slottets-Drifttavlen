// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Bunit;
using Bunit.TestDoubles;

using Core.Interfaces.Services;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using Page = WebUI.Client.Components.Pages.Gdpr.AnonymizationQueuePage;

namespace WebUI.Tests.Components.Pages.Gdpr;

public class AnonymizationQueuePageTests : Bunit.TestContext
{
    private readonly Mock<IAnonymizationService> _service = new();

    public AnonymizationQueuePageTests()
    {
        TestAuthorizationContext auth = this.AddTestAuthorization();
        auth.SetAuthorized("admin@example.com");
        auth.SetRoles("admin");
        _ = _service.Setup(s => s.GetCandidatesAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);
        _ = Services.AddScoped(_ => _service.Object);
    }

    [Fact]
    public void Render_AsAdmin_ShowsHeading()
    {
        IRenderedComponent<Page> cut = RenderComponent<Page>();
        cut.WaitForAssertion(() => Assert.Contains("Anonymization Queue", cut.Markup));
    }

    [Fact]
    public void Render_AsAdmin_LoadsCandidates()
    {
        IRenderedComponent<Page> cut = RenderComponent<Page>();
        cut.WaitForAssertion(() => _service.Verify(s => s.GetCandidatesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce));
    }
}
