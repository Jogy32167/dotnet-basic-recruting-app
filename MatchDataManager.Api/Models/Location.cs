using SQLite;
using System.ComponentModel.DataAnnotations;

namespace MatchDataManager.Api.Models;

public class Location : Entity
{
    public string Name { get; set; }
    public string City { get; set; }
}
