using System.Collections.Generic;

namespace JurassicCode.API.DTOs;

public class ZoneDto
{
    public string Name { get; set; }
    public bool IsOpen { get; set; }
    public string Status { get; set; }
    public int DinosaurCount { get; set; }
    public int CarnivoreCount { get; set; }
    public int HerbivoreCount { get; set; }
    public int SickDinosaurCount { get; set; }
    
    public List<DinosaurDto> Dinosaurs { get; set; } = new List<DinosaurDto>();
}