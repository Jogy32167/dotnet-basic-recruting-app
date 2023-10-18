using MatchDataManager.Api.Models;
using MatchDataManager.Api.Repositories.Impl;

namespace MatchDataManager.Tests
{

    public class TeamsRepositoryTests
    {
        private DbClient _dbClient;

        [SetUp]
        public void Setup()
        {
           // _dbClient = new DbClient(":memory:");
        }

        [Test]
        public async Task Adding_team_should_create_id()
        {
            var dbClient = new DbClient(":memory:");
            var underTesting = new TeamsRepository(dbClient);
            var myTeam = new Team { Name = "PZPN", CoachName = "Probierz" };

            var addResult = await underTesting.AddTeam(myTeam);
            var dbTeam = await underTesting.GetTeamById(myTeam.Id);

            Assert.That(addResult, Is.EqualTo(1));
            Assert.That(dbTeam.Name, Is.EqualTo("PZPN"));
            Assert.That(dbTeam.CoachName, Is.EqualTo("Probierz"));
        }

        [Test]
        public async Task Getting_wrong_team_should_return_0()
        {
            var dbClient = new DbClient(":memory:");
            var underTesting = new TeamsRepository(dbClient);
            var myTeam = new Team { Name = "PZPN", CoachName = "Probierz" };

            var addResult = await underTesting.AddTeam(myTeam);

            Assert.ThrowsAsync<InvalidOperationException>(async () => await underTesting.GetTeamById(Guid.NewGuid()));
        }

        [Test]
        public async Task Updating_team_should_change_coachName()
        {
            var dbClient = new DbClient(":memory:");
            var underTesting = new TeamsRepository(dbClient);
            var myTeam = new Team { Name = "PZPN", CoachName = "Probierz" };

            await underTesting.AddTeam(myTeam);

            myTeam.CoachName = "Mourinho";
            var updateResult = await underTesting.UpdateTeam(myTeam);

            var dbTeam = await underTesting.GetTeamById(myTeam.Id);

            Assert.That(updateResult, Is.EqualTo(1));
            Assert.That(dbTeam.Name, Is.EqualTo("PZPN"));
            Assert.That(dbTeam.CoachName, Is.EqualTo("Mourinho"));
        }
    }
}