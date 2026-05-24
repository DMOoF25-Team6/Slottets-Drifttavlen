// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Bunit;
using Bunit.TestDoubles;

using Core.Interfaces.Services;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using Page = WebUI.Client.Components.Pages.Management.Residents.Residents;

namespace WebUI.Tests.Components.Pages.Management;

public class ResidentsPageTests : Bunit.TestContext
{
    private readonly Mock<IResidentService> _service = new();

    public ResidentsPageTests()
    {
        TestAuthorizationContext auth = this.AddTestAuthorization();
        auth.SetAuthorized("admin@example.com");
        auth.SetRoles("admin");
        _ = _service.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);
        _ = Services.AddScoped(_ => _service.Object);
    }

    [Fact]
    public void Render_AsAdmin_ShowsHeading()
    {
        IRenderedComponent<Page> cut = RenderComponent<Page>();
        cut.WaitForAssertion(() => Assert.Contains("Beboere", cut.Markup));
    }

    [Fact]
    public void Render_AsAdmin_LoadsResidents()
    {
        IRenderedComponent<Page> cut = RenderComponent<Page>();
        cut.WaitForAssertion(() => _service.Verify(s => s.GetAllAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce));
    }
}
