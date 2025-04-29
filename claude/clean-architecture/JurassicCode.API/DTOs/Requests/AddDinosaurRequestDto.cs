using System.ComponentModel.DataAnnotations;

namespace JurassicCode.API.DTOs.Requests;

public class AddDinosaurRequestDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Species { get; set; }
}