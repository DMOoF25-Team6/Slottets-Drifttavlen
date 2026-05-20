// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;
using Domain.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

public class TaskListConfiguration : IEntityTypeConfiguration<TaskList>
{
        public void Configure(EntityTypeBuilder<TaskList> builder)
        {

        builder.ToTable("TaskLists");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(50);
        builder.Property(t => t.Description)
                .HasMaxLength(200);
        builder.Property(t => t.TaskStatus)
                .IsRequired();
        builder.Property(t => t.DueTime)
                .IsRequired();
        builder.Property(t => t.Department)
                .IsRequired();
        SeedingData(builder);
        }

    public void SeedingData(EntityTypeBuilder<TaskList> builder)
    {
        _ = builder.HasData(
            new TaskList
            {
                Id = Guid.Parse("12345678-1234-5678-1234-567812345678"),
                Title = "indkøb",
                Description = "Tasks to be completed in the morning.",
                TaskStatus = TaskListStatus.InProgress,
                DueTime = DateTime.Now.AddHours(2),
                Department = Department.Skoven
            },
            new TaskList
            {
                Id = Guid.Parse("87654321-4321-8765-4321-876543218765"),
                Title = "rengøring",
                Description = "Tasks to be completed in the afternoon.",
                TaskStatus = TaskListStatus.InProgress,
                DueTime = DateTime.Now.AddHours(4),
                Department = Department.Skoven
            },
            new TaskList
            {
                Id = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                Title = "lave aftensmad",
                Description = "Tasks to be completed in the evening.",
                TaskStatus = TaskListStatus.InProgress,
                DueTime = DateTime.Now.AddHours(6),
                Department = Department.Skoven
            },
            new TaskList
            {
                Id = Guid.Parse("12345678-1234-5678-1234-598812345678"),
                Title = "indkøb",
                Description = "Tasks to be completed in the morning.",
                TaskStatus = TaskListStatus.InProgress,
                DueTime = DateTime.Now.AddHours(2),
                Department = Department.Slottet
            },
            new TaskList
            {
                Id = Guid.Parse("87654321-4321-8765-4321-598543218765"),
                Title = "rengøring",
                Description = "Tasks to be completed in the afternoon.",
                TaskStatus = TaskListStatus.InProgress,
                DueTime = DateTime.Now.AddHours(4),
                Department = Department.Slottet
            },
            new TaskList
            {
                Id = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF20"),
                Title = "lave aftensmad",
                Description = "Tasks to be completed in the evening.",
                TaskStatus = TaskListStatus.InProgress,
                DueTime = DateTime.Now.AddHours(6),
                Department = Department.Slottet
            },
            new TaskList
            {
                Id = Guid.Parse("65345678-1234-5678-1234-598812345678"),
                Title = "indkøb",
                Description = "Tasks to be completed in the morning.",
                TaskStatus = TaskListStatus.InProgress,
                DueTime = DateTime.Now.AddHours(2),
                Department = Department.Marken
            },
            new TaskList
            {
                Id = Guid.Parse("87695221-4321-8765-4321-598543218765"),
                Title = "rengøring",
                Description = "Tasks to be completed in the afternoon.",
                TaskStatus = TaskListStatus.InProgress,
                DueTime = DateTime.Now.AddHours(4),
                Department = Department.Marken
            },
            new TaskList
            {
                Id = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF30"),
                Title = "lave aftensmad",
                Description = "Tasks to be completed in the evening.",
                TaskStatus = TaskListStatus.InProgress,
                DueTime = DateTime.Now.AddHours(6),
                Department = Department.Marken
            }
        );

    }
}