// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Mappers;

using Domain.Entities;

namespace Core.Tests.Mappers;

public class PhoneAssignmentMapperTests
{
    [Fact]
    [Trait("Category", "Functionality")]
    public void ToDto_MapsPhoneNumberAndShift()
    {
        PhoneAssignment entity = new() { PhoneNumber = "12345678", ShiftType = "Day" };

        PhoneAssignmentDto dto = PhoneAssignmentMapper.ToDto(entity);

        Assert.Equal("12345678", dto.PhoneNumber);
        Assert.Equal("Day", dto.ShiftType);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public void ToDtos_MapsCollection()
    {
        PhoneAssignment[] entities =
        [
            new() { PhoneNumber = "111", ShiftType = "Day" },
            new() { PhoneNumber = "222", ShiftType = "Night" }
        ];

        List<PhoneAssignmentDto> dtos = PhoneAssignmentMapper.ToDtos(entities).ToList();

        Assert.Equal(2, dtos.Count);
        Assert.Equal("222", dtos[1].PhoneNumber);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void ToDtos_Empty_ReturnsEmpty()
    {
        List<PhoneAssignmentDto> dtos = PhoneAssignmentMapper.ToDtos([]).ToList();

        Assert.Empty(dtos);
    }

    [Fact]
    [Trait("Category", "Concurrency")]
    public void ToDto_ParallelMapping_IsThreadSafe()
    {
        PhoneAssignment entity = new() { PhoneNumber = "999", ShiftType = "Evening" };

        PhoneAssignmentDto[] results = Enumerable.Range(0, 200).AsParallel().Select(_ => PhoneAssignmentMapper.ToDto(entity)).ToArray();

        Assert.All(results, r => Assert.Equal("999", r.PhoneNumber));
    }
}
