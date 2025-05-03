using System;

namespace JurassicCode.API.DTOs;

public class DinosaurDto
{
    public string Name { get; set; }
    public string Species { get; set; }
    public string DietType { get; set; }
    public string HealthStatus { get; set; }
    public DateTime LastFed { get; set; }
    public string TimeSinceLastFed { get; set; }
}