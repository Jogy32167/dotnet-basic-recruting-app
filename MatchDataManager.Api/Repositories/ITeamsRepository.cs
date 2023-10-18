using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Repositories.Impl
{
    public interface ITeamsRepository
    {
        Task<int> AddTeam(Team team);
        Task DeleteTeam(Guid id);
        Task<IEnumerable<Team>> GetAllTeams();
        Task<Team> GetTeamById(Guid id);
        Task<int> UpdateTeam(Team team);
    }
}