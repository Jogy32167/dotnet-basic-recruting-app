using MatchDataManager.Api.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace MatchDataManager.Api.Repositories.Impl
{
    public class DbClient : IDbClient
    {
        string _dbPath;
        private SQLiteAsyncConnection _conn;
        private readonly SemaphoreSlim _semaphore;

        public DbClient(string dbPath)
        {
            _dbPath = dbPath;
            _semaphore = new SemaphoreSlim(1, 1);
        }

        private async Task Init()
        {
            if (_conn != null)
                return;

            _conn = new SQLiteAsyncConnection(_dbPath);
            await _conn.CreateTableAsync<Location>();
            await _conn.CreateTableAsync<Team>();
        }

        public async Task<int> Add(IEntity item)
        {
            await _semaphore.WaitAsync();
            try
            {
                await Init();
                return await _conn.InsertAsync(item);
            }
            catch (Exception ex)
            {
                var e = ex;
                return 0;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<int> Update(IEntity item)
        {
            await _semaphore.WaitAsync();
            try
            {
                await Init();
                return await _conn.UpdateAsync(item);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task Delete<T>(Guid id)
        {
            await _semaphore.WaitAsync();
            try
            {
                await Init();
                await _conn.DeleteAllIdsAsync<T>(new List<string>() { id.ToString() });
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<T> Get<T>(Guid id) where T : new()
        {
            await _semaphore.WaitAsync();
            try
            {
                await Init();
                return await _conn.GetAsync<T>(id);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : new()
        {
            await _semaphore.WaitAsync();
            try
            {
                await Init();
                return await _conn.GetAllWithChildrenAsync<T>();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}