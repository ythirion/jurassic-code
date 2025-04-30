using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using JurassicCode.Requests;
using JurassicCode.Db2;

namespace JurassicCode.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ParkController(ParkService parkService) : ControllerBase
{
    private readonly IParkService _parkService = parkService;

    [HttpGet("GetAllZones")]
    [HttpOptions("GetAllZones")] // Add OPTIONS support for CORS preflight
    public IActionResult GetAllZones()
    {
        try
        {
            var zones = _parkService.GetAllZones();
            return Ok(zones);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving zones: {ex.Message}");
        }
    }

    [HttpPost("AddZone")]
    public IActionResult AddZone([FromBody] ZoneRequest request)
    {
        try
        {
            _parkService.AddZone(request.Name, request.IsOpen);
            return Ok("Zone added successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error adding zone: {ex.Message}");
        }
    }

    [HttpPost("AddDinosaurToZone")]
    public IActionResult AddDinosaurToZone([FromBody] AddDinosaurRequest request)
    {
        try
        {
            _parkService.AddDinosaurToZone(request.ZoneName, request.Dinosaur);
            return Ok("Dinosaur added to zone successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error adding dinosaur to zone: {ex.Message}");
        }
    }

    [HttpPost("MoveDinosaur")]
    public IActionResult MoveDinosaur([FromBody] MoveDinosaurRequest request)
    {
        try
        {
            _parkService.MoveDinosaur(request.FromZoneName, request.ToZoneName, request.DinosaurName);
            return Ok("Dinosaur moved successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error moving dinosaur: {ex.Message}");
        }
    }

    [HttpPost("ToggleZone")]
    public IActionResult ToggleZone([FromBody] ZoneToggleRequest request)
    {
        try
        {
            if (request.ZoneNames.Count == 1)
            {
                _parkService.ToggleZone(request.ZoneNames[0]);
            }
            else
            {
                _parkService.ToggleZones(request.ZoneNames);
            }
            return Ok("Zones toggled successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error toggling zones: {ex.Message}");
        }
    }

    [HttpPost("CanSpeciesCoexist")]
    public IActionResult CanSpeciesCoexist([FromBody] SpeciesCoexistRequest request)
    {
        try
        {
            bool canCoexist = _parkService.CanSpeciesCoexist(request.Species1, request.Species2);
            return Ok(canCoexist);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error determining species coexistence: {ex.Message}");
        }
    }

    [HttpPost("GetDinosaursInZone")]
    public IActionResult GetDinosaursInZone([FromBody] ZoneRequest request)
    {
        try
        {
            var dinosaurs = _parkService.GetDinosaursInZone(request.Name);
            return Ok(dinosaurs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving dinosaurs: {ex.Message}");
        }
    }
}