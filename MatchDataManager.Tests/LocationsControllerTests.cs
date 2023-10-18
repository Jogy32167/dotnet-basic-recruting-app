using MatchDataManager.Api.Controllers;
using MatchDataManager.Api.Models;
using MatchDataManager.Api.Repositories.Impl;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Tests
{
    public class LocationsControllerTests
    {
        private DbClient _dbClient;

        [SetUp]
        public void Setup()
        {
            _dbClient = new DbClient(":memory:");
        }

        [Test]
        public async Task Added_location_name_should_be_unique()
        {
            var locationsRepo = new LocationsRepository(_dbClient);
            var underTesting = new LocationsController(locationsRepo);

            var myLocation1 = new Location { Name = "Narodowy", City = "Warszawa" };
            var myLocation2 = new Location { Name = "Narodowy", City = "Gdansk" };

            var addResult1 = await underTesting.AddLocationAsync(myLocation1);
            var addResult2 = await underTesting.AddLocationAsync(myLocation2);

            Assert.That(addResult1, Is.InstanceOf<CreatedAtActionResult>());
            Assert.That(addResult2, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Added_location_name_should_be_max_255_long()
        {
            var locationsRepo = new LocationsRepository(_dbClient);
            var underTesting = new LocationsController(locationsRepo);

            var name = string.Empty;
            for (int i = 0; i < 256; i++)
            {
                name = name + "x";
            }

            var myLocation = new Location { Name = name, City = "Warszawa" };

            var addResult = await underTesting.AddLocationAsync(myLocation);

            Assert.That(addResult, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Added_location_city_should_be_max_55_long()
        {
            var locationsRepo = new LocationsRepository(_dbClient);
            var underTesting = new LocationsController(locationsRepo);

            var city = string.Empty;
            for (int i = 0; i < 56; i++)
            {
                city = city + "x";
            }

            var myLocation = new Location { Name = "Narodowy", City = city };

            var addResult = await underTesting.AddLocationAsync(myLocation);

            Assert.That(addResult, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Added_location_name_should_be_min_1_long()
        {
            var locationsRepo = new LocationsRepository(_dbClient);
            var underTesting = new LocationsController(locationsRepo);

            var myLocation = new Location { Name = "", City = "Warszawa" };

            var addResult = await underTesting.AddLocationAsync(myLocation);

            Assert.That(addResult, Is.InstanceOf<BadRequestObjectResult>());
        }
        
        [Test]
        public async Task Added_location_city_should_be_min_1_long()
        {
            var locationsRepo = new LocationsRepository(_dbClient);
            var underTesting = new LocationsController(locationsRepo);

            var myLocation = new Location { Name = "Narodowy", City = "" };

            var addResult = await underTesting.AddLocationAsync(myLocation);

            Assert.That(addResult, Is.InstanceOf<BadRequestObjectResult>());
        }

    }
}