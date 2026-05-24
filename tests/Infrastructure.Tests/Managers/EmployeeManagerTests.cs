// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;

using Core.DTOs;

using Infrastructure.Managers;

namespace Infrastructure.Tests.Managers;

public class EmployeeManagerTests : HttpManagerTestBase
{
    private readonly EmployeeManager _manager;

    public EmployeeManagerTests() => _manager = new EmployeeManager(FactoryMock.Object);

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetAllAsync_DeserialisesPayload()
    {
        EmployeeDto[] payload = [new(), new()];
        Setup("employees", Json(HttpStatusCode.OK, payload));

        IEnumerable<EmployeeDto> result = await _manager.GetAllAsync(CancellationToken.None);

        Assert.Equal(2, result.Count());
    }
}
