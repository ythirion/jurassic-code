using System.ComponentModel.DataAnnotations;

namespace JurassicCode.API.DTOs.Requests;

public class MoveDinosaurRequestDto
{
    [Required]
    public string DinosaurName { get; set; }
    
    [Required]
    public string FromZoneName { get; set; }
    
    [Required]
    public string ToZoneName { get; set; }
}