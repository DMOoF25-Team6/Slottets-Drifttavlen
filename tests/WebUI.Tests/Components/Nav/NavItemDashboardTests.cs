// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Bunit;
using Bunit.TestDoubles;

using Component = WebUI.Client.Components.NavItemDashboard;

namespace WebUI.Tests.Components.Nav;

public class NavItemDashboardTests : Bunit.TestContext
{
    [Fact]
    public void Render_WhenAuthenticated_RendersWithoutError()
    {
        TestAuthorizationContext auth = this.AddTestAuthorization();
        auth.SetAuthorized("admin@example.com");
        auth.SetRoles("admin");

        IRenderedComponent<Component> cut = RenderComponent<Component>();

        Assert.NotNull(cut);
    }

    [Fact]
    public void Render_WhenNotAuthenticated_RendersWithoutError()
    {
        TestAuthorizationContext auth = this.AddTestAuthorization();
        auth.SetNotAuthorized();

        IRenderedComponent<Component> cut = RenderComponent<Component>();

        Assert.NotNull(cut);
    }
}
