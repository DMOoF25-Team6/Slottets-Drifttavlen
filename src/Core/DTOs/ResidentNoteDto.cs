// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.
namespace Core.DTOs;


/// <summary>
/// Data Transfer Object for sending resident note data to the client (dashboard).
/// Contains only the information needed by the frontend.
/// </summary>
/// 
public class ResidentNoteDto
{
    // The unique identifier of the resident note.
    public Guid Id { get; set; }

    public string Note { get; set; } = string.Empty;

    public DateTime DateTime { get; set; }

    public string Initials { get; set; } = string.Empty;
}
