using System.ComponentModel.DataAnnotations;

namespace JurassicCode.API.DTOs.Requests;

public class SpeciesCompatibilityRequestDto
{
    [Required]
    public string Species1 { get; set; }
    
    [Required]
    public string Species2 { get; set; }
}