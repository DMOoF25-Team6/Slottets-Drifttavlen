using Bunit;
using Bunit.TestDoubles;
using System.Security.Claims;
using WebUI.Client.Components;
using Xunit;

namespace WebUI.Tests.Components;

public class CurrentUserStatusTests : Bunit.TestContext
{
    [Fact]
    public void ShowsEmail_WhenAuthenticated_WithEmailClaim()
    {
        TestAuthorizationContext authContext = this.AddTestAuthorization();
        authContext.SetAuthorized("Test User");
        authContext.SetClaims(new Claim(ClaimTypes.Email, "test@example.com"));

        IRenderedComponent<CurrentUserStatus> cut = RenderComponent<CurrentUserStatus>();

        Assert.Contains("test@example.com", cut.Markup);
    }

    [Fact]
    public void ShowsName_WhenAuthenticated_WithoutEmailClaim()
    {
        TestAuthorizationContext authContext = this.AddTestAuthorization();
        authContext.SetAuthorized("Test User");

        IRenderedComponent<CurrentUserStatus> cut = RenderComponent<CurrentUserStatus>();

        Assert.Contains("Test User", cut.Markup);
    }

    [Fact]
    public void ShowsNotLoggedIn_WhenNotAuthenticated()
    {
        TestAuthorizationContext authContext = this.AddTestAuthorization();
        authContext.SetNotAuthorized();

        IRenderedComponent<CurrentUserStatus> cut = RenderComponent<CurrentUserStatus>();

        Assert.Contains("Ikke logget ind", cut.Markup);
    }
}
