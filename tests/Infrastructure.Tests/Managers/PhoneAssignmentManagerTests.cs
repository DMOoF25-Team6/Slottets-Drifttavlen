// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;

using Core.DTOs;

using Infrastructure.Managers;

namespace Infrastructure.Tests.Managers;

public class PhoneAssignmentManagerTests : HttpManagerTestBase
{
    private readonly PhoneAssignmentManager _manager;

    public PhoneAssignmentManagerTests() => _manager = new PhoneAssignmentManager(FactoryMock.Object);

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetActiveShiftAsync_DeserialisesPayload()
    {
        PhoneAssignmentDto[] payload = [new()];
        Setup("phoneassignments/active-shift", Json(HttpStatusCode.OK, payload));

        IEnumerable<PhoneAssignmentDto> result = await _manager.GetCurrentPhoneAssignmentsForActiveShift(CancellationToken.None);

        _ = Assert.Single(result);
    }
}
