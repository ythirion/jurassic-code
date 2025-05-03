using Microsoft.AspNetCore.Mvc;
using JurassicCode.API.DTOs.Requests;
using JurassicCode.API.Mappers;
using JurassicCode.Application.UseCases.Dinosaurs;
using JurassicCode.Application.UseCases.Zones;
using JurassicCode.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JurassicCode.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParkController : ControllerBase
{
    private readonly ILogger<ParkController> _logger;
    private readonly GetAllZonesUseCase _getAllZonesUseCase;
    private readonly GetZoneByNameUseCase _getZoneByNameUseCase;
    private readonly CreateZoneUseCase _createZoneUseCase;
    private readonly ToggleZoneStatusUseCase _toggleZoneStatusUseCase;
    private readonly GetDinosaursInZoneUseCase _getDinosaursInZoneUseCase;
    private readonly AddDinosaurToZoneUseCase _addDinosaurToZoneUseCase;
    private readonly MoveDinosaurUseCase _moveDinosaurUseCase;
    private readonly SpeciesCompatibilityUseCase _speciesCompatibilityUseCase;

    public ParkController(
        ILogger<ParkController> logger,
        GetAllZonesUseCase getAllZonesUseCase,
        GetZoneByNameUseCase getZoneByNameUseCase,
        CreateZoneUseCase createZoneUseCase,
        ToggleZoneStatusUseCase toggleZoneStatusUseCase,
        GetDinosaursInZoneUseCase getDinosaursInZoneUseCase,
        AddDinosaurToZoneUseCase addDinosaurToZoneUseCase,
        MoveDinosaurUseCase moveDinosaurUseCase,
        SpeciesCompatibilityUseCase speciesCompatibilityUseCase)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _getAllZonesUseCase = getAllZonesUseCase ?? throw new ArgumentNullException(nameof(getAllZonesUseCase));
        _getZoneByNameUseCase = getZoneByNameUseCase ?? throw new ArgumentNullException(nameof(getZoneByNameUseCase));
        _createZoneUseCase = createZoneUseCase ?? throw new ArgumentNullException(nameof(createZoneUseCase));
        _toggleZoneStatusUseCase = toggleZoneStatusUseCase ?? throw new ArgumentNullException(nameof(toggleZoneStatusUseCase));
        _getDinosaursInZoneUseCase = getDinosaursInZoneUseCase ?? throw new ArgumentNullException(nameof(getDinosaursInZoneUseCase));
        _addDinosaurToZoneUseCase = addDinosaurToZoneUseCase ?? throw new ArgumentNullException(nameof(addDinosaurToZoneUseCase));
        _moveDinosaurUseCase = moveDinosaurUseCase ?? throw new ArgumentNullException(nameof(moveDinosaurUseCase));
        _speciesCompatibilityUseCase = speciesCompatibilityUseCase ?? throw new ArgumentNullException(nameof(speciesCompatibilityUseCase));
    }

    [HttpGet("zones")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllZones()
    {
        try
        {
            var zones = await _getAllZonesUseCase.ExecuteAsync();
            return Ok(zones.ToDtos());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving zones");
            return StatusCode(500, "An error occurred while retrieving zones");
        }
    }
    
    [HttpGet("zones/{zoneName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetZoneByName(string zoneName)
    {
        try
        {
            var zone = await _getZoneByNameUseCase.ExecuteAsync(zoneName);
            return Ok(zone.ToDto());
        }
        catch (ZoneDomainException ex)
        {
            _logger.LogWarning(ex, "Zone not found: {ZoneName}", zoneName);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving zone: {ZoneName}", zoneName);
            return StatusCode(500, "An error occurred while retrieving the zone");
        }
    }

    [HttpPost("zones")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateZone([FromBody] CreateZoneRequestDto request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var zone = await _createZoneUseCase.ExecuteAsync(
                new Application.UseCases.Zones.CreateZoneRequest(request.Name, request.IsOpen));
                
            return CreatedAtAction(
                nameof(GetZoneByName), 
                new { zoneName = zone.Name }, 
                zone.ToDto());
        }
        catch (ZoneDomainException ex)
        {
            _logger.LogWarning(ex, "Invalid operation when creating zone");
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid request when creating zone");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating zone");
            return StatusCode(500, "An error occurred while creating the zone");
        }
    }

    [HttpPatch("zones/{zoneName}/toggle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ToggleZone(string zoneName)
    {
        try
        {
            var zone = await _toggleZoneStatusUseCase.ExecuteAsync(zoneName);
            return Ok(zone.ToDto());
        }
        catch (ZoneDomainException ex)
        {
            _logger.LogWarning(ex, "Zone not found: {ZoneName}", zoneName);
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid request when toggling zone");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error toggling zone status");
            return StatusCode(500, "An error occurred while toggling the zone status");
        }
    }

    [HttpGet("zones/{zoneName}/dinosaurs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDinosaursInZone(string zoneName)
    {
        try
        {
            var dinosaurs = await _getDinosaursInZoneUseCase.ExecuteAsync(zoneName);
            return Ok(dinosaurs.ToDtos());
        }
        catch (ZoneDomainException ex)
        {
            _logger.LogWarning(ex, "Zone not found: {ZoneName}", zoneName);
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid request when retrieving dinosaurs");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving dinosaurs");
            return StatusCode(500, "An error occurred while retrieving dinosaurs");
        }
    }

    [HttpPost("zones/{zoneName}/dinosaurs")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddDinosaurToZone(
        string zoneName, 
        [FromBody] AddDinosaurRequestDto request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dinosaur = await _addDinosaurToZoneUseCase.ExecuteAsync(
                new AddDinosaurToZoneRequest(zoneName, request.Name, request.Species));
                
            return CreatedAtAction(
                nameof(GetDinosaursInZone), 
                new { zoneName }, 
                dinosaur.ToDto());
        }
        catch (ZoneDomainException ex)
        {
            _logger.LogWarning(ex, "Zone error: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (DinosaurDomainException ex)
        {
            _logger.LogWarning(ex, "Dinosaur error: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid request when adding dinosaur");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding dinosaur to zone");
            return StatusCode(500, "An error occurred while adding the dinosaur to the zone");
        }
    }

    [HttpPost("dinosaurs/move")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MoveDinosaur([FromBody] MoveDinosaurRequestDto request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dinosaur = await _moveDinosaurUseCase.ExecuteAsync(
                new MoveDinosaurRequest(
                    request.DinosaurName,
                    request.FromZoneName,
                    request.ToZoneName));
                
            return Ok(dinosaur.ToDto());
        }
        catch (ZoneDomainException ex)
        {
            _logger.LogWarning(ex, "Zone error: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (DinosaurDomainException ex)
        {
            _logger.LogWarning(ex, "Dinosaur error: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid request when moving dinosaur");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error moving dinosaur");
            return StatusCode(500, "An error occurred while moving the dinosaur");
        }
    }

    [HttpPost("species/compatibility")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CheckSpeciesCompatibility([FromBody] SpeciesCompatibilityRequestDto request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var compatible = await _speciesCompatibilityUseCase.ExecuteAsync(
                new SpeciesCompatibilityRequest(request.Species1, request.Species2));
                
            return Ok(new { compatible });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid request when checking species compatibility");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking species compatibility");
            return StatusCode(500, "An error occurred while checking species compatibility");
        }
    }
}