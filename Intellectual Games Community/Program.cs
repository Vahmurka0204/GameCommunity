using Library;
using System;
using System.Collections.Generic;

namespace Intellectual_Games_Community
{
    class Program
    {
        static void Main(string[] args)
        {
            Container.Register<IOracle, Oracle>();
            Player p = new Player("a");
            var team = new Team(new List<Player> { p }, "T");
            Console.ReadKey();
           
        }

    }
}
