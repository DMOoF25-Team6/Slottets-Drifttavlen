// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Bunit;

using Core.DTOs;

using Component = WebUI.Client.Components.Residents.PainkillerStatus;

namespace WebUI.Tests.Components.Residents;

public class PainkillerStatusTests : Bunit.TestContext
{
    [Fact]
    public void Render_WithStatus_RendersWithoutError()
    {
        PainkillerStatusDto status = new() { ResidentId = Guid.NewGuid(), Types = ["Paracetamol"], NextAllowedTime = DateTime.UtcNow.AddHours(2) };

        IRenderedComponent<Component> cut = RenderComponent<Component>(p => p.Add(c => c.Status, status));

        Assert.NotNull(cut);
    }

    [Fact]
    public void Render_WithNullStatus_RendersWithoutError()
    {
        IRenderedComponent<Component> cut = RenderComponent<Component>(p => p.Add(c => c.Status, (PainkillerStatusDto?)null));

        Assert.NotNull(cut);
    }
}
