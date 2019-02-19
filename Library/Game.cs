using System.Collections.Generic;

namespace Library
{
    public class Game
    {
        public string Name;
        public string Date;
        public Dictionary<Team, int> Results;
        public List<Team> Teams;
        public int NumberOfQuestions;

        public Game(string name, string date, int number)
        {
            Name = name;
            Date = date;
            NumberOfQuestions = number;
        }
    }

}
