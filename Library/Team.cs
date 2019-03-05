using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Library
{
    public class Team
    {
        public TeamCaptain Captain;
        public List<Player> TeamMembers;
        public int Rating;
        public string Name;
        private IOracle _oracle;


        public Team(List<Player> players, string name)
        {
            Captain = null;
            TeamMembers = players;
            Name = name;
            Rating = 0;
            _oracle = Container.Resolve<IOracle>();
        }

        public void ChooseCaptain()
        {
            int index = _oracle.MakeChoice(0, TeamMembers.Count - 1);

            if (Captain == null)
            {
                Captain = new TeamCaptain(TeamMembers[index].Name);
            }
            else
            {
                Captain.Name = TeamMembers[index].Name;
            }

        }

        public void ChangeOracle(IOracle oracle)
        {
            _oracle = oracle;
        }

        public void AddPlayer(Player player)
        {
            TeamMembers.Add(player);

        }

        public void ApplyForParticipation(Game game)
        {
            game.Teams.Add(this);
        }

        public void OnNewGameAvailable(Game game)
        {
            Debug.Write(game.Name);
        }
    }

}
