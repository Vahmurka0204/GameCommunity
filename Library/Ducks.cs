using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{

    public class Duck
    {
        public IFlyBehavior FlyBehavior;
        public IQuackBehavior QuackBehavior;

        public void Swim()
        {
            Console.WriteLine("~Swimming~");
        }

        public void PerformQuack()
        {
            QuackBehavior.Quack();
        }

        public virtual void Display() { }

        public void PerformFly()
        {
            FlyBehavior.Fly();
        }

        public void SetFlyBehavior(IFlyBehavior flyBehavior)
        {
            FlyBehavior = flyBehavior;
        }

        public void SetQuackBehavior(IQuackBehavior quackBehavior)
        {
            QuackBehavior = quackBehavior;
        }
       
    }

    public class MallarDuck: Duck
    {
        public MallarDuck():base()
        {
            QuackBehavior = new Squeak();
            FlyBehavior = new FlyWithWings();
        }

        public override void Display()
        {
            Console.WriteLine("I'm Mallar Duck!");
        }
    }

    public class DecoyDuck: Duck
    {
        public DecoyDuck(): base()
        {
            QuackBehavior = new MuteQuack();
            FlyBehavior = new FlyNoWay();
        }

        public override void Display()
        {
            Console.WriteLine("This is Decoy Duck.");
        }
}

    public interface IFlyBehavior
    {
        void Fly();
    }

    public class FlyWithWings : IFlyBehavior
    {
        public void Fly()
        {
            Console.WriteLine("I'm flying");
        }
    }

    public class FlyNoWay : IFlyBehavior
    {
        public void Fly()
        {
            Console.WriteLine("This can't fly :(");
        }
    }

    public interface IQuackBehavior
    {
        void Quack();
    }

    public class Squeak : IQuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("Squuueeeaak!");
        }
        
    }

    public class MuteQuack : IQuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("Silence..");
        }
    }

}
