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

    public interface IC
    {

    }

    public class A: IA
    {
    public A(IB b) { }
       
    }

    public class B: IB
    {
        
    }

   public class C: IC
    {
        public C( string c) { }
    }

    public class D: IC
    {
        public D(IA a, int d) { }
    }
}
