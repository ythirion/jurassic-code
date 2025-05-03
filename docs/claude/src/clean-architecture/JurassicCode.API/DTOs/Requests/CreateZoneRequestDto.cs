using System.ComponentModel.DataAnnotations;

namespace JurassicCode.API.DTOs.Requests;

public class CreateZoneRequestDto
{
    [Required]
    public string Name { get; set; }
    
    public bool IsOpen { get; set; }
}