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
            var castMethod = typeof(Container).GetMethod("Cast").MakeGenericMethod(typeof(IB));
            object castedValue = castMethod.Invoke(null, new object[] { new B() });

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
