using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Intellectual_Games_Community
{
    class Program
    {
        static void Main(string[] args)
        {
            var Donald = new MallarDuck();
            var Daisy = new DecoyDuck();

            Donald.Display();
            Donald.PerformFly();
            Donald.PerformQuack();

            Donald.SetFlyBehavior(new FlyNoWay());
            Donald.SetQuackBehavior(new MuteQuack());

            Donald.PerformFly();
            Donald.PerformQuack();

            Console.WriteLine();

            Daisy.Display();
            Daisy.PerformFly();
            Daisy.PerformQuack();

            Daisy.SetQuackBehavior(new Squeak());
            Daisy.SetFlyBehavior(new FlyWithWings());

            Daisy.PerformFly();
            Daisy.PerformQuack();

            Console.ReadKey();
        }

        

        

    }
}
