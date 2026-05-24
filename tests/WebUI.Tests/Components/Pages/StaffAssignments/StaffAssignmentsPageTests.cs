// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;
using System.Text;

using Bunit;
using Bunit.TestDoubles;

using Core.Interfaces.Services;

using Microsoft.Extensions.DependencyInjection;

using Moq;
using Moq.Protected;

using Page = WebUI.Client.Components.Pages.StaffAssignments.StaffAssignments;

namespace WebUI.Tests.Components.Pages.StaffAssignments;

public class StaffAssignmentsPageTests : Bunit.TestContext
{
    private readonly Mock<IResidentService> _residents = new();
    private readonly Mock<IHttpClientFactory> _factory = new();

    public StaffAssignmentsPageTests()
    {
        TestAuthorizationContext auth = this.AddTestAuthorization();
        auth.SetAuthorized("admin@example.com");
        auth.SetRoles("admin");

        Mock<HttpMessageHandler> handler = new();
        _ = handler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[]", Encoding.UTF8, "application/json")
            });
        HttpClient client = new(handler.Object) { BaseAddress = new Uri("http://localhost/") };
        _ = _factory.Setup(f => f.CreateClient("SlottetApi")).Returns(client);
        _ = _factory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(client);

        _ = _residents.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);

        _ = Services.AddScoped(_ => _residents.Object);
        _ = Services.AddScoped(_ => _factory.Object);
    }

    [Fact]
    public void Render_ShowsHeading()
    {
        IRenderedComponent<Page> cut = RenderComponent<Page>();
        cut.WaitForAssertion(() => Assert.Contains("Staff Assignments", cut.Markup));
    }
}
