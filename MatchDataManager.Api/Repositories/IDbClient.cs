using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Repositories.Impl
{
    public interface IDbClient
    {
        Task<int> Add(IEntity item);
        Task<int> Update(IEntity item);
        Task Delete<T>(Guid id);
        Task<T> Get<T>(Guid id) where T : new();
        Task<IEnumerable<T>> GetAll<T>() where T : new();
    }
}