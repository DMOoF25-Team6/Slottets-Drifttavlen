// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Enums;

namespace Core.Helpers;

public static class ShiftTypeHelper
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
