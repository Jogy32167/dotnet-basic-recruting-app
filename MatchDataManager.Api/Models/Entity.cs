using SQLite;

namespace MatchDataManager.Api.Models;

public class Entity : IEntity
{
    [PrimaryKey]
    public Guid Id { get; set; }
}