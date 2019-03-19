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
            //var b = new B();
            //IB ib = Container.Cast<IB>(b);

            //var castMethod = typeof(Container).GetMethod("Cast").MakeGenericMethod(typeof(IB));
            //object castedValue = castMethod.Invoke(null, new object[] { new B() });

            Container.Register<IA, A>();
            Container.Register<IB, B>();
            Container.Resolve<IA>();
            
            Console.ReadKey();


        }

        public interface IA
        {

        }

        public interface IB
        {
            int MyProperty { get; set; }
        }

        public class A : IA
        {
            public A(IB b) { }

        }

        public class B : IB
        {
            public int MyProperty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }

    }
}
