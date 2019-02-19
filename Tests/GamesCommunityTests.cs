using System;
using System.Collections.Generic;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class GamesCommunityTests
    {
        [TestMethod]
        public void AddPlayer()
        {
            
            var player1 = new Player("a");
            var player2 = new Player("b");
            var player3 = new Player("c");
            var player4 = new Player("d");
            var players = new List<Player> { player1, player2, player3 };
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
            var dreamTeam = new Team(players, "DreamTeam");

            dreamTeam.ChangeOracle(new TestOracle());
            dreamTeam.ChooseCaptain();

            Assert.AreEqual("a", dreamTeam.Captain.Name);
        }
    }
}
