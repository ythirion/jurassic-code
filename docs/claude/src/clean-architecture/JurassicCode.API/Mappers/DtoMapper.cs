using System.Collections.Generic;
using System.Linq;
using JurassicCode.API.DTOs;
using JurassicCode.Domain.Entities;
using JurassicCode.Domain.ValueObjects;

namespace JurassicCode.API.Mappers;

/// <summary>
/// Mapper for converting domain entities to DTOs
/// </summary>
public static class DtoMapper
{
    public static ZoneDto ToDto(this Zone zone)
    {
        if (zone == null)
            return null;
            
        return new ZoneDto
        {
            Name = zone.Name,
            IsOpen = zone.Status == ZoneStatus.Open,
            Status = zone.Status.ToString(),
            DinosaurCount = zone.CountDinosaurs(),
            CarnivoreCount = zone.CountDangerousDinosaurs(),
            HerbivoreCount = zone.CountDinosaurs() - zone.CountDangerousDinosaurs(),
            SickDinosaurCount = zone.CountSickDinosaurs(),
            Dinosaurs = zone.Dinosaurs.Select(d => d.ToDto()).ToList()
        };
    }
    
    public static IEnumerable<ZoneDto> ToDtos(this IEnumerable<Zone> zones)
    {
        return zones?.Select(ToDto);
    }
    
    public static DinosaurDto ToDto(this Dinosaur dinosaur)
    {
        if (dinosaur == null)
            return null;
            
        return new DinosaurDto
        {
            Name = dinosaur.Name,
            Species = dinosaur.Species.Name,
            DietType = dinosaur.Species.Diet.ToString(),
            HealthStatus = dinosaur.HealthStatus.ToString(),
            LastFed = dinosaur.LastFed,
            TimeSinceLastFed = FormatTimeSpan(dinosaur.GetTimeSinceLastFed())
        };
    }
    
    public static IEnumerable<DinosaurDto> ToDtos(this IEnumerable<Dinosaur> dinosaurs)
    {
        return dinosaurs?.Select(ToDto);
    }
    
    private static string FormatTimeSpan(System.TimeSpan timeSpan)
    {
        if (timeSpan.TotalDays >= 1)
            return $"{(int)timeSpan.TotalDays} day(s)";
        else if (timeSpan.TotalHours >= 1)
            return $"{(int)timeSpan.TotalHours} hour(s)";
        else
            return $"{(int)timeSpan.TotalMinutes} minute(s)";
    }
}