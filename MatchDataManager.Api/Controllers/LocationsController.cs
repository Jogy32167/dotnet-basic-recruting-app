using MatchDataManager.Api.Models;
using MatchDataManager.Api.Repositories.Impl;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationsRepository _locationsRepository;

    public LocationsController(ILocationsRepository locationsRepository)
    {
        _locationsRepository = locationsRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AddLocationAsync(Location location)
    {
        var validation = await ValidateLocationAsync(location);
        if (validation != null)
        {
            return validation;
        }

        var result = await _locationsRepository.AddLocation(location);
        if (result != 1)
        {
            return Problem("Adding to local repo failed.", statusCode: StatusCodes.Status304NotModified);
        }

        return CreatedAtAction(nameof(GetById), new {id = location.Id}, location);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLocationAsync(Guid locationId)
    {
        await _locationsRepository.DeleteLocation(locationId);
        return NoContent();
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_locationsRepository.GetAllLocations());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var location = _locationsRepository.GetLocationById(id);
        if (location is null)
        {
            return NotFound();
        }

        return Ok(location);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateLocationAsync(Location location)
    {
        var validation = await ValidateLocationAsync(location);
        if (validation != null)
        {
            return validation;
        }

        var result = await _locationsRepository.UpdateLocation(location);
        if (result != 1)
        {
            return Problem("Updating in local repo failed.", statusCode: StatusCodes.Status304NotModified);
        }

        return Ok(location);
    }

    private async Task<IActionResult> ValidateLocationAsync(Location location)
    {
        if (location.Name.Length == 0)
            return ValidationProblem(new ValidationProblemDetails() { Detail = "Name is required." });
        if (location.Name.Length > 255)
            return ValidationProblem(new ValidationProblemDetails() { Detail = "Name is to long. Max lenght is 255." });
        if (location.City.Length == 0)
            return ValidationProblem(new ValidationProblemDetails() { Detail = "City is required." });
        if (location.City.Length > 55)
            return ValidationProblem(new ValidationProblemDetails() { Detail = "City is to long. Max lenght is 55." });

        var locations = await _locationsRepository.GetAllLocations();
        var sameName = locations.FirstOrDefault(x => x.Name == location.Name);
        if (sameName != null)
        {
            return ValidationProblem(new ValidationProblemDetails() { Detail = "Name already exists." });
        }

        return null;
    }
}