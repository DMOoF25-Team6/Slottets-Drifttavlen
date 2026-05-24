// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Bunit;
using Bunit.TestDoubles;

using Core.Interfaces.Services;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using Page = WebUI.Client.Components.Pages.Gdpr.RetentionSettingsPage;

namespace WebUI.Tests.Components.Pages.Gdpr;

public class RetentionSettingsPageTests : Bunit.TestContext
{
    private readonly Mock<IRetentionPolicyService> _service = new();

    public RetentionSettingsPageTests()
    {
        TestAuthorizationContext auth = this.AddTestAuthorization();
        auth.SetAuthorized("admin@example.com");
        auth.SetRoles("admin");
        _ = _service.Setup(s => s.GetPoliciesAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);
        _ = Services.AddScoped(_ => _service.Object);
    }

    [Fact]
    public void Render_AsAdmin_ShowsHeading()
    {
        IRenderedComponent<Page> cut = RenderComponent<Page>();
        cut.WaitForAssertion(() => Assert.Contains("Retention Settings", cut.Markup));
    }

    [Fact]
    public void Render_AsAdmin_LoadsPolicies()
    {
        IRenderedComponent<Page> cut = RenderComponent<Page>();
        cut.WaitForAssertion(() => _service.Verify(s => s.GetPoliciesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce));
    }
}
