using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Repositories.Impl
{
    public interface ILocationsRepository
    {
        Task<int> AddLocation(Location location);
        Task DeleteLocation(Guid locationId);
        Task<IEnumerable<Location>> GetAllLocations();
        Task<Location> GetLocationById(Guid locationId);
        Task<int> UpdateLocation(Location location);
    }
}