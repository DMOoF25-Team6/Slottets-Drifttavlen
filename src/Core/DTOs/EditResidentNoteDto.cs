// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.



namespace Core.DTOs;

public class EditResidentNoteDto
{
    public Guid ResidentId { get; set; }      // hvilken beboer
    public Guid ResidentNoteId { get; set; }  // hvilken note
    public string NewText { get; set; } = string.Empty;
}
