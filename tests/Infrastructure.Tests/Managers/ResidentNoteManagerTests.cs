// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;

using Core.DTOs;

using Infrastructure.Managers;

namespace Infrastructure.Tests.Managers;

public class ResidentNoteManagerTests : HttpManagerTestBase
{
    private readonly ResidentNoteManager _manager;

    public ResidentNoteManagerTests() => _manager = new ResidentNoteManager(FactoryMock.Object);

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetAllByResidentIdAsync_DeserialisesPayload()
    {
        Guid residentId = Guid.NewGuid();
        ResidentNoteDto[] payload = [new() { Note = "hello" }];
        Setup($"residentnote/{residentId}", Json(HttpStatusCode.OK, payload));

        IEnumerable<ResidentNoteDto> result = await _manager.GetAllByResidentIdAsync(residentId, CancellationToken.None);

        _ = Assert.Single(result);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task AddAsync_WhenOk_ReturnsTrue()
    {
        Setup("residentnote", Status(HttpStatusCode.Created));

        bool ok = await _manager.AddAsync(Guid.NewGuid(), "note", CancellationToken.None);

        Assert.True(ok);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task AddAsync_WhenError_ReturnsFalse()
    {
        Setup("residentnote", Status(HttpStatusCode.BadRequest));

        bool ok = await _manager.AddAsync(Guid.NewGuid(), "note", CancellationToken.None);

        Assert.False(ok);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task UpdateAsync_WhenOk_ReturnsTrue()
    {
        Guid id = Guid.NewGuid();
        Setup($"residentnote/{id}", Status(HttpStatusCode.NoContent));

        bool ok = await _manager.UpdateAsync(id, "new", CancellationToken.None);

        Assert.True(ok);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task DeleteAsync_WhenOk_ReturnsTrue()
    {
        Guid id = Guid.NewGuid();
        Setup($"residentnote/{id}", Status(HttpStatusCode.NoContent));

        bool ok = await _manager.DeleteAsync(id, CancellationToken.None);

        Assert.True(ok);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task DeleteAsync_WhenError_ReturnsFalse()
    {
        Guid id = Guid.NewGuid();
        Setup($"residentnote/{id}", Status(HttpStatusCode.NotFound));

        bool ok = await _manager.DeleteAsync(id, CancellationToken.None);

        Assert.False(ok);
    }
}
