// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Services;

namespace WebApi.Tests.Mocks;

public class MockPhoneAssignmentService : IPhoneAssignmentService
{
    public Task<IEnumerable<PhoneAssignmentDto>> GetCurrentPhoneAssignmentsForActiveShiftAsync(
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<PhoneAssignmentDto>>([]);
    }
}
