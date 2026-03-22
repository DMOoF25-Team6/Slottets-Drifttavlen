// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs;

public class AddResidentNoteDto
{
    public Guid ResidentId { get; set; }
    public string NoteText { get; set; } = string.Empty;


}
