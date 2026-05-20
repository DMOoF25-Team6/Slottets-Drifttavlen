// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities;

public class TaskList : IEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public TaskListStatus TaskStatus { get; set; } = TaskListStatus.InProgress;
    [Required]
    public DateTime DueTime { get; set; }

    [Required]
    public Department Department { get; set; }

}
