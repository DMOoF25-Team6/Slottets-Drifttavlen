// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Bunit;
using Bunit.TestDoubles;

using Core.Interfaces.Services;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using Page = WebUI.Client.Components.Pages.Gdpr.SubjectAccessRequestPage;

namespace WebUI.Tests.Components.Pages.Gdpr;

public class SubjectAccessRequestPageTests : Bunit.TestContext
{
    private readonly Mock<ISubjectAccessRequestService> _service = new();

    public SubjectAccessRequestPageTests()
    {
        TestAuthorizationContext auth = this.AddTestAuthorization();
        auth.SetAuthorized("admin@example.com");
        auth.SetRoles("admin");
        _ = Services.AddScoped(_ => _service.Object);
    }

    [Fact]
    public void Render_AsAdmin_ShowsHeading()
    {
        IRenderedComponent<Page> cut = RenderComponent<Page>();
        cut.WaitForAssertion(() => Assert.Contains("Subject Access Request", cut.Markup));
    }
}
