// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Domain.Enums;

public static class ShiftTypeExtensions
{
    public static string ToDanishString(this ShiftType shiftType)
    {
        return shiftType switch
        {
            ShiftType.Day => "Dag",
            ShiftType.Evening => "Aften",
            ShiftType.Night => "Nat",
            _ => string.Empty
        };
    }
}
