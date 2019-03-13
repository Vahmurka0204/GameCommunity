using Library;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Intellectual_Games_Community
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Container.Register<IOracle, TestOracle>(new TestOracle(new int[]{ 0 }));
             Player p = new Player("a");
             var team = new Team(new List<Player> { p }, "T");*/
            ConstructorInfo[] info = typeof(B).GetConstructors();
            ParameterInfo[] param = info[0].GetParameters();
            if (param==null)
                Console.WriteLine(param[0].ToString());
            else Console.Write("empty constructor");

            
            Console.ReadKey();


        }
        public interface IA
        {

        }

        public interface IB
        {

        }

        public class A : IA
        {
            public A(IB b) { }

        }

        public class B : IB
        {

        }

    }
}
