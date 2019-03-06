using System;
using System.Collections.Generic;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CommunityTests
    {
        [TestMethod]
        public void AddPlayer()
        {
            var player = new Player("a");
            Container.Register<IOracle, TestOracle>(new TestOracle(new int[] { 0 }));
            Team team = new Team(new List<Player> {player}, "TestTeam");
            var community = new Community();

            community.NewGameAvailableEvent += team.OnNewGameAvailable;
            community.CreateGame("TestGame#1", DateTime.Now.ToShortDateString(), 10);

            Assert.AreEqual(1, team.TeamMembers.Count);
        }

        [TestMethod]
        public void ConductGame()
        {
            var player = new Player("a");
            Container.Register<IOracle, Oracle>();
            var team = new Team(new List<Player> { player }, "TestTeam");
            var community = new Community();

            community.CreateGame("TestGame#1", DateTime.Now.ToShortDateString(), 10);
            team.ApplyForParticipation(community.CommunityEvents[0]);
            community.ConductGame(community.CommunityEvents[0]);

            Assert.AreEqual(1, team.Rating);
        }

    }
}
