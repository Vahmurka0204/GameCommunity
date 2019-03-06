using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class Container
    {
        private static  Dictionary<Type, dynamic> _dictionaryOfTypes;
        static Container()
        {
            _dictionaryOfTypes = new Dictionary<Type, dynamic>();
        }

        public static TKey Resolve<TKey>()
        {
            return _dictionaryOfTypes[typeof(TKey)];
        }
                
        public static void Register<TKey>(dynamic t)
        {
            _dictionaryOfTypes[typeof(TKey)] = t;
        }

    }
}
