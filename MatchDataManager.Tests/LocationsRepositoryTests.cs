using MatchDataManager.Api.Models;
using MatchDataManager.Api.Repositories.Impl;

namespace MatchDataManager.Tests
{
    public class LocationsRepositoryTests
    {
        private DbClient _dbClient;

        [SetUp]
        public void Setup()
        {
            _dbClient = new DbClient(":memory:");
        }

        [Test]
        public async Task Adding_location_should_create_id()
        {
            var underTesting = new LocationsRepository(_dbClient);
            var myLocation = new Location { Name = "Narodowy", City = "Warszawa" };

            var addResult = await underTesting.AddLocation(myLocation);
            var dbLocation = await underTesting.GetLocationById(myLocation.Id);

            Assert.That(addResult, Is.EqualTo(1));
            Assert.That(dbLocation.Name, Is.EqualTo("Narodowy"));
            Assert.That(dbLocation.City, Is.EqualTo("Warszawa"));
        }

        [Test]
        public void Getting_wrong_location_should_throw()
        {
            var underTesting = new LocationsRepository(_dbClient);
            Assert.ThrowsAsync<InvalidOperationException>(async () => await underTesting.GetLocationById(Guid.NewGuid()));
        }

        [Test]
        public async Task Updating_should_change_city()
        {
            var underTesting = new LocationsRepository(_dbClient);
            var myLocation = new Location { Name = "Narodowy", City = "Warszawa" };

            await underTesting.AddLocation(myLocation);

            myLocation.Name = "Slaski";
            var updateResult = await underTesting.UpdateLocation(myLocation);

            var dbLocation = await underTesting.GetLocationById(myLocation.Id);

            Assert.That(updateResult, Is.EqualTo(1));
            Assert.That(dbLocation.Name, Is.EqualTo("Slaski"));
            Assert.That(dbLocation.City, Is.EqualTo("Warszawa"));
        }
    }
}