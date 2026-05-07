// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Bunit;

using Core.DTOs;
using Core.Interfaces.Services;

using Domain.Entities;
using Domain.Enums;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using ResidentCardComponent = WebUI.Client.Components.Residents.ResidentCard;

namespace WebUI.Tests.Components.Residents;


/// <summary>
/// Unit tests for the <see cref="ResidentCardComponent"/> Blazor component.
/// </summary>

public class ResidentCardTests : Bunit.TestContext
{
    #region Fields

    private readonly Mock<IResidentNoteService> _residentNoteServiceMock;
    private readonly Mock<IMedicineStatusService> _medicineStatusServiceMock;


    #endregion

    #region Constructor

    public ResidentCardTests()
    {
        _residentNoteServiceMock = new Mock<IResidentNoteService>();
        _medicineStatusServiceMock = new Mock<IMedicineStatusService>();



        _ = Services.AddScoped<IResidentNoteService>(_ => _residentNoteServiceMock.Object);
        _ = Services.AddScoped<IMedicineStatusService>(_ => _medicineStatusServiceMock.Object);



        _ = _residentNoteServiceMock
    .Setup(s => s.GetAllByResidentIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
    .ReturnsAsync([]);

        _ = _medicineStatusServiceMock
            .Setup(s => s.GetMedicineStatusAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new MedicineStatusDto
            {
                ResidentId = Guid.NewGuid(),
                Entries = []
            });

        _ = _medicineStatusServiceMock
            .Setup(s => s.GetPainkillerStatusAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PainkillerStatusDto
            {
                ResidentId = Guid.NewGuid(),
                Types = [],
                NextAllowedTime = DateTime.UtcNow
            });


    }

    #endregion

    #region Render Tests

    [Fact]
    public void Render_WithMedicineStatus_RenderMedicineName()
    {

        // Arrange 
        Guid residentId = Guid.NewGuid();

        Resident resident = new()
        {
            Id = residentId,
            Initials = "AB",
            TrafficLightStatus = TrafficLightStatus.Green,
            Notes = []

        };

        _ = _residentNoteServiceMock
           .Setup(s => s.GetAllByResidentIdAsync(residentId, It.IsAny<CancellationToken>()))
           .ReturnsAsync([]);

        _ = _medicineStatusServiceMock
            .Setup(s => s.GetMedicineStatusAsync(residentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new MedicineStatusDto
            {
                ResidentId = residentId,
                Entries =
                [
                    new MedicineEntryDto
                    {
                        Name = "Paracetamol",
                        Given = true,
                        Timestamp = new DateTime(2026, 04, 24, 10, 00, 00)
                    }
                ]
            });

        _ = _medicineStatusServiceMock
            .Setup(s => s.GetPainkillerStatusAsync(residentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PainkillerStatusDto
            {
                ResidentId = residentId,
                Types = ["Morphine"],
                NextAllowedTime = new DateTime(2026, 04, 24, 12, 00, 00)
            });

        // Act
        IRenderedComponent<ResidentCardComponent> cut = RenderComponent<ResidentCardComponent>(
            parameters => parameters.Add(p => p.Resident, resident));

        // Assert
        cut.WaitForAssertion(() => Assert.Contains("Paracetamol", cut.Markup));

    }



    [Fact]
    public void Render_WithMedicineGiven_RendersYes()
    {
        // Arrange 

        Guid residentId = Guid.NewGuid();
        Resident resident = new()
        {
            Id = residentId,
            Initials = "AB",
            TrafficLightStatus = TrafficLightStatus.Green,
            Notes = []
        };

        _ = _residentNoteServiceMock
            .Setup(s => s.GetAllByResidentIdAsync(residentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        _ = _medicineStatusServiceMock
            .Setup(s => s.GetMedicineStatusAsync(residentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new MedicineStatusDto
            {
                ResidentId = residentId,
                Entries =
                [
                    new MedicineEntryDto
                    {
                        Name = "Paracetamol",
                        Given = true,
                        Timestamp = new DateTime(2026, 04, 24, 10, 00, 00)
                    }
                ]
            });

        _ = _medicineStatusServiceMock
            .Setup(s => s.GetPainkillerStatusAsync(residentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PainkillerStatusDto
            {
                ResidentId = residentId,
                Types = [],
                NextAllowedTime = new DateTime(2026, 04, 24, 12, 00, 00)
            });

        // Act
        IRenderedComponent<ResidentCardComponent> cut = RenderComponent<ResidentCardComponent>(
            parameters => parameters.Add(p => p.Resident, resident));

        // Assert
        cut.WaitForAssertion(() => Assert.Contains("Ja", cut.Markup));
    }

    //[Fact]
    //public void Render_WithPainkillerStatus_RendersTypeAndNextAllowedTime()
    //{
    //    // Arrange
    //    Guid residentId = Guid.NewGuid();

    //    Resident resident = new()
    //    {
    //        Id = residentId,
    //        Initials = "AB",
    //        TrafficLightStatus = TrafficLightStatus.Green,
    //        Notes = []
    //    };

    //    _ = _residentNoteServiceMock
    //        .Setup(s => s.GetAllByResidentIdAsync(residentId, It.IsAny<CancellationToken>()))
    //        .ReturnsAsync([]);

    //    _ = _medicineStatusServiceMock
    //        .Setup(s => s.GetMedicineStatusAsync(residentId, It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(new MedicineStatusDto
    //        {
    //            ResidentId = residentId,
    //            Entries = new List<MedicineEntryDto>()
    //        });

    //    _ = _medicineStatusServiceMock
    //        .Setup(s => s.GetPainkillerStatusAsync(residentId, It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(new PainkillerStatusDto
    //        {
    //            ResidentId = residentId,
    //            Types = ["Morphine"],
    //            NextAllowedTime = new DateTime(2026, 04, 24, 12, 00, 00)
    //        });

    //    // Act
    //    IRenderedComponent<ResidentCardComponent> cut = RenderComponent<ResidentCardComponent>(
    //        parameters => parameters.Add(p => p.Resident, resident));

    //    // Assert
    //    cut.WaitForAssertion(() =>
    //    {
    //        Assert.Contains("Morphine", cut.Markup);
    //        Assert.Contains("12:00 24-04-2026", cut.Markup);
    //    });
    //}

    [Fact]
    public void Render_WithoutMedicineStatus_RendersEmptyMedicineMessage()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();

        Resident resident = new()
        {
            Id = residentId,
            Initials = "AB",
            TrafficLightStatus = TrafficLightStatus.Green,
            Notes = []
        };

        _ = _residentNoteServiceMock
            .Setup(s => s.GetAllByResidentIdAsync(residentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        _ = _medicineStatusServiceMock
            .Setup(s => s.GetMedicineStatusAsync(residentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new MedicineStatusDto
            {
                ResidentId = residentId,
                Entries = []
            });

        _ = _medicineStatusServiceMock
            .Setup(s => s.GetPainkillerStatusAsync(residentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PainkillerStatusDto
            {
                ResidentId = residentId,
                Types = [],
                NextAllowedTime = new DateTime(2026, 04, 24, 12, 00, 00)
            });

        // Act
        IRenderedComponent<ResidentCardComponent> cut = RenderComponent<ResidentCardComponent>(
            parameters => parameters.Add(p => p.Resident, resident));

        // Assert
        cut.WaitForAssertion(() => Assert.Contains("Ingen medicinregistreringer", cut.Markup));
    }

    #endregion
}
