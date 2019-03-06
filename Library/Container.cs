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
        private static Dictionary<Type, dynamic> _dictionaryTypeParameter;

        static Container()
        {
            _dictionaryOfTypes = new Dictionary<Type, Type>();
            _dictionaryTypeParameter = new Dictionary<Type, dynamic>();
        }

        public static TKey Resolve<TKey>()
        {
            if (_dictionaryOfTypes.ContainsKey(typeof(TKey))==true)
            {
                return (TKey)Activator.CreateInstance(_dictionaryOfTypes[typeof(TKey)]);
            }
            else
            return _dictionaryTypeParameter[typeof(TKey)];
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
