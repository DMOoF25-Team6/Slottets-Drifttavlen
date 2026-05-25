// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Bunit;
using Bunit.TestDoubles;

using Core.DTOs;
using Core.Interfaces.Services;

using Domain.Entities;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using Component = WebUI.Client.Components.Residents.ResidentCard;

namespace WebUI.Tests.Components.Residents;

public class ResidentCardExtraTests : Bunit.TestContext
{
    private readonly Mock<IResidentNoteService> _notes = new();
    private readonly Mock<IMedicineStatusService> _medicine = new();

    public ResidentCardExtraTests()
    {
        TestAuthorizationContext auth = this.AddTestAuthorization();
        auth.SetAuthorized("admin@example.com");
        auth.SetRoles("admin");

        _ = _notes.Setup(s => s.GetAllByResidentIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync([]);
        _ = _medicine.Setup(s => s.GetMedicineStatusAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(new MedicineStatusDto());
        _ = _medicine.Setup(s => s.GetPainkillerStatusAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PainkillerStatusDto());

        _ = Services.AddScoped(_ => _notes.Object);
        _ = Services.AddScoped(_ => _medicine.Object);
    }

    [Fact]
    public void Render_WithResident_ShowsInitials()
    {
        Resident resident = new() { Id = Guid.NewGuid(), Initials = "Q:WW" };

        IRenderedComponent<Component> cut = RenderComponent<Component>(p => p.Add(c => c.Resident, resident));

        cut.WaitForAssertion(() => Assert.Contains("Q:WW", cut.Markup));
    }

    [Fact]
    public void Render_WithResident_LoadsMedicineStatus()
    {
        Resident resident = new() { Id = Guid.NewGuid(), Initials = "AB" };

        IRenderedComponent<Component> cut = RenderComponent<Component>(p => p.Add(c => c.Resident, resident));

        cut.WaitForAssertion(() =>
            _medicine.Verify(s => s.GetMedicineStatusAsync(resident.Id, It.IsAny<CancellationToken>()), Times.AtLeastOnce));
    }
}
