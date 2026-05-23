// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace WebUI.Client.Models;

public class ResidentFormModel
{
    [Required(ErrorMessage = "Initialer er paakraevet.")]
    [MaxLength(2, ErrorMessage = "Initialer maa hoejst vaere 2 tegn.")]
    public string Initials { get; set; } = string.Empty;

    [Required(ErrorMessage = "Fornavn er paakraevet.")]
    [MaxLength(50, ErrorMessage = "Fornavn maa hoejst vaere 50 tegn.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Efternavn er paakraevet.")]
    [MaxLength(50, ErrorMessage = "Efternavn maa hoejst vaere 50 tegn.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Afdeling er paakraevet.")]
    public Department Department { get; set; }

    public TrafficLightStatus? TrafficLightStatus { get; set; }

    [MaxLength(100, ErrorMessage = "Aktivitet maa hoejst vaere 100 tegn.")]
    public string? Activity { get; set; }

    [MaxLength(50, ErrorMessage = "Ledsager maa hoejst vaere 50 tegn.")]
    public string? Companion { get; set; }

    [MaxLength(20, ErrorMessage = "Beloeb maa hoejst vaere 20 tegn.")]
    public string? Amount { get; set; }

    [MaxLength(200, ErrorMessage = "Info maa hoejst vaere 200 tegn.")]
    public string? Info { get; set; }
}
