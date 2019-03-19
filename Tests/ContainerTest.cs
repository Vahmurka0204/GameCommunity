using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;


namespace Tests
{
    
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        public void NeedRegisterAnotherPairFoResolve()
        {
            Container.Register<IA, A>();
            var c = Container.Resolve<IA>();
        }

        [TestMethod]
        public void ResolveWithCreatingNewItem()
        {
            Container.Register<IA, A>();
            Container.Register<IB, B>();
            var c = Container.Resolve<IA>();
        }

        [TestMethod]
        public void ResolveSingltone()
        {
            Container.Register<IA, A>();
            Container.RegisterSingltone<IB, B>();
            var c = Container.Resolve<IA>();
        }

        [TestMethod]
        public void ResolveItemWithParameter()
        {
            Container.Register<IA, A>();
            Container.Register<IB, B>(new B());
            var c = Container.Resolve<IA>();
        }

        [TestMethod]
        public void ResolveReregister()
        {
            Container.Register<IA, A>();
            Container.Register<IB, B>(new B());
            Container.RegisterSingltone<IB, B>();
            var c = Container.Resolve<IA>();
        }

        [TestMethod]
        public void AnotherTypeInConstructor()
        {
            Container.Register<IC, C>();
            var c = Container.Resolve<IC>();
        }

        [TestMethod]
        public void ConstructorInterfaceInt()
        {
            Container.Register<IC, D>();
            var c = Container.Resolve<IC>();
        }

        [TestMethod]
        public void RegisterWrongPair()
        {
            Container.Register<int,int>();
            var c = Container.Resolve<int>();
        }
    }
}
