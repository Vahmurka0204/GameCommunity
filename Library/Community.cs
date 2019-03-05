using System;
using System.Collections.Generic;

namespace Library
{
    public delegate void NewGameAvailableHandler(Game e);

    public class Community
    {
        public event NewGameAvailableHandler NewGameAvailableEvent;
        //public event EventHandler New1GameAvailableEvent;
        //public event EventHandler<Game> New2GameAvailableEvent;
        

        public List<Game> CommunityEvents;

        public Community()
        {
            CommunityEvents = new List<Game>();
        }

        public void CreateGame(string name,string date, int numberQuestion)
        {
            Game newGame = new Game(name, date, numberQuestion);
            CommunityEvents.Add( newGame);

            if(NewGameAvailableEvent != null)
            {
                NewGameAvailableEvent.Invoke(newGame);
            }
        }

        public void ConductGame(Game game)
        {
            foreach(Team team in game.Teams)
            {
                team.Rating++;
            }
        }
    }
}
