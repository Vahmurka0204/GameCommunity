using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public interface IA
    {

    }

    public interface IB
    {

    }

    public class A: IA
    {
    public A(IB b) { }
       
    }

    public class B: IB
    {

    }

    class DI
    {
        ConstructorInfo f = g;
    }
}
