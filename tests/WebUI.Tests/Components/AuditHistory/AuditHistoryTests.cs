// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Bunit;

using Core.DTOs.Audit;
using Core.Interfaces.Managers;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using AuditHistoryComponent = WebUI.Client.Components.Pages.AuditHistory.AuditHistory;

namespace WebUI.Tests.Components.AuditHistory;

/// <summary>
/// Unit tests for the <see cref="AuditHistoryComponent"/> Blazor page.
/// </summary>
/// <remarks>
/// Verifies UI behaviour for UC-009 (View History and Traceability): rendering of the
/// audit list, filter/limit interactions, the change-details modal, and badge styling.
/// Uses bUnit + Moq to mock <see cref="IAuditManager"/> and isolate the component from HTTP.
/// </remarks>
public class AuditHistoryTests : Bunit.TestContext
{
    #region Fields

    private readonly Mock<IAuditManager> _auditManagerMock;

    #endregion

    #region Constructor

    public AuditHistoryTests()
    {
        _auditManagerMock = new Mock<IAuditManager>();

        _ = Services.AddScoped(_ => _auditManagerMock.Object);

        // Default: empty list so OnInitializedAsync doesn't throw.
        _ = _auditManagerMock
            .Setup(m => m.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        _ = _auditManagerMock
            .Setup(m => m.GetByEntityNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
    }

    #endregion

    #region Helpers

    private static AuditEntryDto CreateDto(
        string entity = "Resident",
        string changeType = "Added",
        Guid? id = null,
        string userName = "")
    {
        return new AuditEntryDto
        {
            Id = id ?? Guid.NewGuid(),
            Entity = entity,
            ChangeType = changeType,
            Description = $"{entity} - {changeType}",
            EventTimeUtc = new DateTime(2026, 5, 9, 10, 0, 0, DateTimeKind.Utc),
            RegisteredTimeUtc = new DateTime(2026, 5, 9, 10, 0, 1, DateTimeKind.Utc),
            UserId = Guid.NewGuid(),
            UserName = userName,
            Succeeded = true,
            ChangeDetails = []
        };
    }

    #endregion

    #region Render Tests

    [Fact]
    public void Render_WhenNoEntries_RendersEmptyMessage()
    {
        // Arrange (default mock returns empty list)

        // Act
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();

        // Assert
        cut.WaitForAssertion(() =>
            Assert.Contains("Ingen hændelser fundet", cut.Markup));
    }

    [Fact]
    public void Render_WhenEntriesExist_RendersTableWithRows()
    {
        // Arrange
        AuditEntryDto[] entries =
        [
            CreateDto("Resident", "Added"),
            CreateDto("ResidentNote", "Modified")
        ];
        _ = _auditManagerMock
            .Setup(m => m.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);

        // Act
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();

        // Assert
        cut.WaitForAssertion(() =>
        {
            Assert.NotNull(cut.Find("table"));
            Assert.Contains("Resident", cut.Markup);
            Assert.Contains("ResidentNote", cut.Markup);
        });
    }

    [Fact]
    public void Render_Always_RendersHeading()
    {
        // Act
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();

        // Assert
        cut.WaitForAssertion(() => Assert.Contains("Historik", cut.Markup));
    }

    [Fact]
    public void Render_Always_RendersDashboardBackLink()
    {
        // Act
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();

        // Assert
        cut.WaitForAssertion(() =>
            Assert.Contains("Tilbage til dashboard", cut.Markup));
    }

    [Fact]
    public void Render_WhenManagerThrows_RendersErrorMessage()
    {
        // Arrange
        _ = _auditManagerMock
            .Setup(m => m.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("Connection failed"));

        // Act
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();

        // Assert
        cut.WaitForAssertion(() =>
            Assert.Contains("Kunne ikke hente historik", cut.Markup));
    }

    [Fact]
    public void Render_WithUserName_RendersUserName()
    {
        // Arrange
        AuditEntryDto[] entries = [CreateDto(userName: "PederRasmussen")];
        _ = _auditManagerMock
            .Setup(m => m.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);

        // Act
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();

        // Assert
        cut.WaitForAssertion(() => Assert.Contains("PederRasmussen", cut.Markup));
    }

    [Fact]
    public void Render_WithoutUserName_RendersDashPlaceholder()
    {
        // Arrange
        AuditEntryDto[] entries = [CreateDto(userName: "")];
        _ = _auditManagerMock
            .Setup(m => m.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);

        // Act
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();

        // Assert
        cut.WaitForAssertion(() => Assert.Contains("—", cut.Markup));
    }

    #endregion

    #region Initial Load Tests

    [Fact]
    public void OnInitialized_AnyState_CallsGetRecentAsyncWithDefaultLimit()
    {
        // Act
        _ = RenderComponent<AuditHistoryComponent>();

        // Assert: default limit is 50
        _auditManagerMock.Verify(
            m => m.GetRecentAsync(50, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public void OnInitialized_AnyState_DoesNotCallGetByEntityName()
    {
        // Act
        _ = RenderComponent<AuditHistoryComponent>();

        // Assert
        _auditManagerMock.Verify(
            m => m.GetByEntityNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    #endregion

    #region Filter Tests

    [Fact]
    public void EntityFilterChanged_WithEntityName_CallsGetByEntityName()
    {
        // Arrange
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();
        cut.WaitForAssertion(() => Assert.NotNull(cut.Find("#entityFilter")));

        // Act
        cut.Find("#entityFilter").Change("Resident");

        // Assert
        cut.WaitForAssertion(() =>
            _auditManagerMock.Verify(
                m => m.GetByEntityNameAsync("Resident", It.IsAny<CancellationToken>()),
                Times.Once));
    }

    [Fact]
    public void EntityFilterChanged_BackToAllTypes_CallsGetRecentAgain()
    {
        // Arrange
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();
        cut.WaitForAssertion(() => Assert.NotNull(cut.Find("#entityFilter")));

        // Act: select an entity then go back to "all types" (empty value)
        cut.Find("#entityFilter").Change("Resident");
        cut.Find("#entityFilter").Change("");

        // Assert: GetRecentAsync called both at init and after going back to "all types"
        cut.WaitForAssertion(() =>
            _auditManagerMock.Verify(
                m => m.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()),
                Times.AtLeast(2)));
    }

    [Fact]
    public void Render_FilterDropdown_ContainsExpectedOptions()
    {
        // Act
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();

        // Assert
        cut.WaitForAssertion(() =>
        {
            string markup = cut.Markup;
            Assert.Contains("Resident", markup);
            Assert.Contains("ResidentNote", markup);
            Assert.Contains("PhoneAssignment", markup);
            Assert.Contains("MedicineRecord", markup);
            Assert.Contains("PainkillerRecord", markup);
        });
    }

    #endregion

    #region Limit Tests

    [Fact]
    public void LimitChanged_WithValidNumber_CallsGetRecentWithNewLimit()
    {
        // Arrange
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();
        cut.WaitForAssertion(() => Assert.NotNull(cut.Find("#limitInput")));

        // Act
        cut.Find("#limitInput").Change("100");

        // Assert
        cut.WaitForAssertion(() =>
            _auditManagerMock.Verify(
                m => m.GetRecentAsync(100, It.IsAny<CancellationToken>()),
                Times.Once));
    }

    [Fact]
    public void LimitChanged_WithInvalidValue_DoesNotReload()
    {
        // Arrange
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();
        cut.WaitForAssertion(() => Assert.NotNull(cut.Find("#limitInput")));
        _auditManagerMock.Invocations.Clear();

        // Act: non-numeric input keeps the current limit
        cut.Find("#limitInput").Change("not-a-number");

        // Assert
        _auditManagerMock.Verify(
            m => m.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public void LimitChanged_WithZero_DoesNotReload()
    {
        // Arrange
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();
        cut.WaitForAssertion(() => Assert.NotNull(cut.Find("#limitInput")));
        _auditManagerMock.Invocations.Clear();

        // Act: zero is not a positive value, so the page does not reload
        cut.Find("#limitInput").Change("0");

        // Assert
        _auditManagerMock.Verify(
            m => m.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    #endregion

    #region Detail Modal Tests

    [Fact]
    public void ClickDetailsButton_WhenEntryExists_CallsGetWithDetails()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        AuditEntryDto[] entries = [CreateDto(id: id)];
        _ = _auditManagerMock
            .Setup(m => m.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);
        _ = _auditManagerMock
            .Setup(m => m.GetWithDetailsAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateDto(id: id));

        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();
        cut.WaitForAssertion(() => Assert.Contains("Detaljer", cut.Markup));

        // Act
        cut.FindAll("button").First(b => b.TextContent.Contains("Detaljer")).Click();

        // Assert
        cut.WaitForAssertion(() =>
            _auditManagerMock.Verify(
                m => m.GetWithDetailsAsync(id, It.IsAny<CancellationToken>()),
                Times.Once));
    }

    [Fact]
    public void ClickDetailsButton_WhenEntryHasChangeDetails_RendersChangeDetailsTable()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        AuditEntryDto[] entries = [CreateDto(id: id)];
        AuditEntryDto withDetails = CreateDto(id: id);
        withDetails.ChangeDetails =
        [
            new ChangeDetailDto { Field = "Initials", OldValue = "AB", NewValue = "CD" }
        ];

        _ = _auditManagerMock
            .Setup(m => m.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);
        _ = _auditManagerMock
            .Setup(m => m.GetWithDetailsAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(withDetails);

        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();
        cut.WaitForAssertion(() => Assert.Contains("Detaljer", cut.Markup));

        // Act
        cut.FindAll("button").First(b => b.TextContent.Contains("Detaljer")).Click();

        // Assert
        cut.WaitForAssertion(() =>
        {
            Assert.Contains("Felt-ændringer", cut.Markup);
            Assert.Contains("Initials", cut.Markup);
            Assert.Contains("AB", cut.Markup);
            Assert.Contains("CD", cut.Markup);
        });
    }

    [Fact]
    public void ClickDetailsButton_WhenNoChangeDetails_RendersFallbackMessage()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        AuditEntryDto[] entries = [CreateDto(id: id)];
        _ = _auditManagerMock
            .Setup(m => m.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);
        _ = _auditManagerMock
            .Setup(m => m.GetWithDetailsAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateDto(id: id)); // ChangeDetails empty by default

        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();
        cut.WaitForAssertion(() => Assert.Contains("Detaljer", cut.Markup));

        // Act
        cut.FindAll("button").First(b => b.TextContent.Contains("Detaljer")).Click();

        // Assert
        cut.WaitForAssertion(() =>
            Assert.Contains("Ingen felt-ændringer registreret", cut.Markup));
    }

    [Fact]
    public void ClickCloseButton_AfterOpeningModal_HidesModal()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        AuditEntryDto[] entries = [CreateDto(id: id)];
        _ = _auditManagerMock
            .Setup(m => m.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);
        _ = _auditManagerMock
            .Setup(m => m.GetWithDetailsAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateDto(id: id));

        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();
        cut.WaitForAssertion(() => Assert.Contains("Detaljer", cut.Markup));

        // Open modal
        cut.FindAll("button").First(b => b.TextContent.Contains("Detaljer")).Click();
        cut.WaitForAssertion(() => Assert.Contains("Detaljer for hændelse", cut.Markup));

        // Act: close modal via "Luk" button
        cut.FindAll("button").First(b => b.TextContent.Contains("Luk")).Click();

        // Assert
        cut.WaitForAssertion(() =>
            Assert.DoesNotContain("Detaljer for hændelse", cut.Markup));
    }

    #endregion

    #region Badge Styling Tests

    [Theory]
    [InlineData("Added", "bg-success")]
    [InlineData("Modified", "bg-warning")]
    [InlineData("Deleted", "bg-danger")]
    [InlineData("UnknownType", "bg-secondary")]
    public void Render_WithChangeType_AppliesCorrectBadgeClass(string changeType, string expectedCssClass)
    {
        // Arrange
        AuditEntryDto[] entries = [CreateDto(changeType: changeType)];
        _ = _auditManagerMock
            .Setup(m => m.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);

        // Act
        IRenderedComponent<AuditHistoryComponent> cut = RenderComponent<AuditHistoryComponent>();

        // Assert
        cut.WaitForAssertion(() => Assert.Contains(expectedCssClass, cut.Markup));
    }

    #endregion
}
