using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Repositories.Impl;

public class TeamsRepository : ITeamsRepository
{
    private readonly IDbClient _dbClient;

    public TeamsRepository(IDbClient dbClient)
    {
        _dbClient = dbClient;
    }

    public async Task<int> AddTeam(Team team)
    {
        team.Id = Guid.NewGuid();
        return await _dbClient.Add(team);
    }

    public async Task DeleteTeam(Guid teamId)
    {
        await _dbClient.Delete<Team>(teamId);
    }

    public async Task<IEnumerable<Team>> GetAllTeams()
    {
        return await _dbClient.GetAll<Team>();
    }

    public async Task<Team> GetTeamById(Guid id)
    {
        return await _dbClient.Get<Team>(id);
    }

    public async Task<int> UpdateTeam(Team team)
    {
        return await _dbClient.Update(team);
    }
}