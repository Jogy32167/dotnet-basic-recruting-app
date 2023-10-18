using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Repositories.Impl
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly IDbClient _dbClient;

        public LocationsRepository(IDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        public async Task<int> AddLocation(Location location)
        {
            location.Id = Guid.NewGuid();
            return await _dbClient.Add(location);
        }

        public async Task DeleteLocation(Guid locationId)
        {
            await _dbClient.Delete<Location>(locationId);
        }

        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            return await _dbClient.GetAll<Location>();
        }

        public async Task<Location> GetLocationById(Guid id)
        {
            return await _dbClient.Get<Location>(id);
        }

        public async Task<int> UpdateLocation(Location location)
        {
            return await _dbClient.Update(location);
        }
    }
}