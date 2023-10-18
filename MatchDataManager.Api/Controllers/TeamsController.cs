using MatchDataManager.Api.Models;
using MatchDataManager.Api.Repositories.Impl;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamsRepository _teamsRepository;

    public TeamsController(ITeamsRepository teamsRepository)
    {
        _teamsRepository = teamsRepository;
    }

    [HttpPost]
    public IActionResult AddTeam(Team team)
    {
        _teamsRepository.AddTeam(team);
        return CreatedAtAction(nameof(GetById), new {id = team.Id}, team);
    }

    [HttpDelete]
    public IActionResult DeleteTeam(Guid teamId)
    {
        _teamsRepository.DeleteTeam(teamId);
        return NoContent();
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_teamsRepository.GetAllTeams());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var location = _teamsRepository.GetTeamById(id);
        if (location is null)
        {
            return NotFound();
        }

        return Ok(location);
    }

    [HttpPut]
    public IActionResult UpdateTeam(Team team)
    {
        _teamsRepository.UpdateTeam(team);
        return Ok(team);
    }
}