using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace WebUI.Client.Models;

/// <summary>
/// Form model with validation attributes for the create/edit resident modal.
/// </summary>
public class ResidentFormModel
{
    [Required(ErrorMessage = "Initialer er påkrævet.")]
    [MaxLength(2, ErrorMessage = "Initialer må højst være 2 tegn.")]
    public string Initials { get; set; } = string.Empty;

    [Required(ErrorMessage = "Fornavn er påkrævet.")]
    [MaxLength(50, ErrorMessage = "Fornavn må højst være 50 tegn.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Efternavn er påkrævet.")]
    [MaxLength(50, ErrorMessage = "Efternavn må højst være 50 tegn.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Afdeling er påkrævet.")]
    public Department Department { get; set; }

    public TrafficLightStatus? TrafficLightStatus { get; set; }
}
