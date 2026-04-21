// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;

namespace WebApi.Tests.Mocks;

public class MockPhoneAssignmentManager : IPhoneAssignmentManager
{
    public Task<IEnumerable<PhoneAssignmentDto>> GetCurrentPhoneAssignmentsForActiveShift(
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<PhoneAssignmentDto>>([]);
    }
}
