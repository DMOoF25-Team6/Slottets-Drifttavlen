// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Dto.Identity;

namespace Core.DTOs.Identity;

public class ErrorDto : ILoginResult, ILogoutResult, IDeleteResult
{
    public IEnumerable<string>? ErrorMessages { get; set; }
}
