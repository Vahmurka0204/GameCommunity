using System;
using System.Collections.Generic;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TeamTests
    {
        [TestMethod]
        public void AddPlayer()
        {
            
            var player1 = new Player("a");
            var player2 = new Player("b");
            var player3 = new Player("c");
            var player4 = new Player("d");
            var players = new List<Player> { player1, player2, player3 };
            Container.RegisterSingltone<IOracle, Oracle>();
            var dreamTeam = new Team(players, "DreamTeam");

            dreamTeam.AddPlayer(player4);

            Assert.AreEqual(4, dreamTeam.TeamMembers.Count);
        }

        [TestMethod]
        public void ChooseCaptain()
        {
            var player1 = new Player("a");
            var player2 = new Player("b");
            var player3 = new Player("c");
            var players = new List<Player> { player1, player2, player3 };
            var oracle = new TestOracle(new int[] { 0});
            Container.Register<IOracle, TestOracle>(oracle);
            var dreamTeam = new Team(players, "DreamTeam");
            
            dreamTeam.ChooseCaptain();

            Assert.AreEqual("a", dreamTeam.Captain.Name);
        }

        [TestMethod]
        public void ChooseCaptainTwoTimes()
        {
            var player1 = new Player("a");
            var player2 = new Player("b");
            Container.Register<IOracle, TestOracle>(new TestOracle(new int[] { 0 }));
            var team = new Team(new List<Player> { player1 }, "TestTeam");

            team.ChooseCaptain();
            team.AddPlayer(player2);
            team.ChooseCaptain();

            Assert.AreEqual("a", team.Captain.Name);
        }
    }
}
