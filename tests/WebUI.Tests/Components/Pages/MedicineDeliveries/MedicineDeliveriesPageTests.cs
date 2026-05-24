// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Bunit;
using Bunit.TestDoubles;

using Core.Interfaces.Services;

using Domain.Entities;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using MedicineDeliveriesPage = WebUI.Client.Components.Pages.Management.MedicineDeliveries.MedicineDeliveries;

namespace WebUI.Tests.Components.Pages.MedicineDeliveries;

/// <summary>
/// bUnit tests for the Medicine Deliveries admin page (UC-020/021/022).
/// </summary>
public class MedicineDeliveriesPageTests : Bunit.TestContext
{
    private readonly Mock<IMedicineDeliveryService> _service = new();

    public MedicineDeliveriesPageTests()
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
        IRenderedComponent<MedicineDeliveriesPage> cut = RenderComponent<MedicineDeliveriesPage>();

        cut.WaitForAssertion(() => Assert.Contains("Medicin leveringer", cut.Markup));
    }

    [Fact]
    public void Render_AsAdmin_LoadsDeliveriesFromService()
    {
        IRenderedComponent<MedicineDeliveriesPage> cut = RenderComponent<MedicineDeliveriesPage>();

        cut.WaitForAssertion(() =>
            _service.Verify(s => s.GetAllAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce));
    }

    [Fact]
    public void Render_WithDelivery_ShowsMedicineName()
    {
        MedicineRecord[] records =
        [
            new() { Id = Guid.NewGuid(), ResidentId = Guid.NewGuid(), MedicineName = "Panodil", Timestamp = DateTime.UtcNow, Given = true }
        ];
        _ = _service.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(records);

        IRenderedComponent<MedicineDeliveriesPage> cut = RenderComponent<MedicineDeliveriesPage>();

        cut.WaitForAssertion(() => Assert.Contains("Panodil", cut.Markup));
    }
}
