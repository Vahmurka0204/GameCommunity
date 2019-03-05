using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class Container
    {
        private static  Dictionary<Type, Type> _dictionaryOfTypes;
        static Container()
        {
            _dictionaryOfTypes = new Dictionary<Type, Type>();
        }

        public static TKey Resolve<TKey>()
        {
            return (TKey)Activator.CreateInstance(_dictionaryOfTypes[typeof(TKey)]);
        }

        public static TKey ResolveWithParametres<TKey>()
        {
            return (TKey)Activator.CreateInstance(_dictionaryOfTypes[typeof(TKey)]);
        }

        public static void Register<TKey, TValue>()
        {
            _dictionaryOfTypes[typeof(TKey)] = typeof(TValue);
        }

    }
}
