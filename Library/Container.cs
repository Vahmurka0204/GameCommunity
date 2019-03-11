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
        private static Dictionary<Type, object> _dictionaryTypeParameter;
        private static Dictionary<Type, object> _dictionarySingltone;

        static Container()
        {
            _dictionaryOfTypes = new Dictionary<Type, Type>();
            _dictionaryTypeParameter = new Dictionary<Type, object>();
            _dictionarySingltone = new Dictionary<Type, object>();
        }

        public static TKey Resolve<TKey>()
        {
            if (_dictionarySingltone.ContainsKey(typeof(TKey)) == false)
            {
                if (_dictionaryOfTypes.ContainsKey(typeof(TKey)) == true)
                {
                    _dictionarySingltone[typeof(TKey)] = Activator.CreateInstance(_dictionaryOfTypes[typeof(TKey)]);
                }
                else
                    _dictionarySingltone[typeof(TKey)] = _dictionaryTypeParameter[typeof(TKey)];

            }
            return (TKey)_dictionarySingltone[typeof(TKey)];
        }
                
        public  static void Register<TKey, TValue>(TValue t)
        {
            
            _dictionaryTypeParameter[typeof(TKey)] = t;
        }

        public static void Register<TKey, TValue>()
        {

            _dictionaryOfTypes[typeof(TKey)] =typeof(TValue);
        }

    }
}
