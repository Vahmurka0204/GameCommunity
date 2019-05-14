using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;


namespace Tests
{
    [TestClass]
    public class ContainerTest
    {
        [TestCleanup]
        public void TestCleanUp()
        {
            Container.Clean();
        }
        [TestMethod]
        [ExpectedException(typeof(ContainerException))]
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
        [ExpectedException(typeof(ContainerException))]
        public void AnotherTypeInConstructor()
        {
            Container.Register<IC, C>();
            var c = Container.Resolve<IC>();
        }

        [TestMethod]
        [ExpectedException(typeof(ContainerException))]
        public void ConstructorInterfaceInt()
        {
            Container.Register<IC, D>();
            var c = Container.Resolve<IC>();
        }

        [TestMethod]
        [ExpectedException(typeof(ContainerException))]
        public void RegisterWrongPair()
        {
            Container.Register<int, int>();
            var c = Container.Resolve<int>();
        }

        [TestMethod]
        public void CreateSingltone()
        {
            Container.RegisterSingltone<IB, B>();
            var b1 = Container.Resolve<IB>();
            var b2 = Container.Resolve<IB>();
            Assert.AreSame(b1, b2);
        }

        [TestMethod]
        public void DifferentLinks()
        {
            Container.Register<IB, B>();
            var b1 = Container.Resolve<IB>();
            var b2 = Container.Resolve<IB>();
            Assert.AreNotSame(b1, b2);
        }

        [TestMethod]
        public void CreateParameter()
        {
            var b = new B();
            Container.Register<IB, B>(b);
            var b1 = Container.Resolve<IB>();
            Assert.AreSame(b, b1);
        }

       
    }
}
