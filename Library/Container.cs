using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Library
{
    public static class Container
    {
        private static  Dictionary<Type, Type> _dictionaryOfTypes;
        private static Dictionary<Type, object> _dictionaryTypeParameter;
        private static Dictionary<Type, object> _dictionarySingltone;
        private static HashSet<Type> _allKeys;

        static Container()
        {
            _dictionaryOfTypes = new Dictionary<Type, Type>();
            _dictionaryTypeParameter = new Dictionary<Type, object>();
            _dictionarySingltone = new Dictionary<Type, object>();
        }

        public static TKey Resolve<TKey>()
        {
            ConstructorInfo[] ctorInfo = typeof(TKey).GetConstructors();
            
            foreach(ConstructorInfo c in ctorInfo)
            {
                ParameterInfo[] parameterInfo = c.GetParameters();
                if (parameterInfo == null)
                {
                    break;
                }
                foreach(ParameterInfo p in parameterInfo)
                {
                    if (_allKeys.Contains(p.ParameterType))
                    {
                        Type t = p.ParameterType;
                        Resolve<t>();
                    }
                }
            }
            if (_dictionarySingltone.ContainsKey(typeof(TKey)) == false)
            {
                if (_dictionaryOfTypes.ContainsKey(typeof(TKey)) == true)
                {
                    return (TKey)Activator.CreateInstance(_dictionaryOfTypes[typeof(TKey)]);
                }
                else if (_dictionaryTypeParameter.ContainsKey(typeof(TKey)) == true)
                {
                    return (TKey)_dictionaryTypeParameter[typeof(TKey)];
                }

            }
            return (TKey)_dictionarySingltone[typeof(TKey)];
        }
                
        public  static void Register<TKey, TValue>(TValue t)
        {
            _allKeys.Add(typeof(TKey));
            _dictionaryTypeParameter[typeof(TKey)] = t;
        }

        public static void Register<TKey, TValue>()
        {
            _allKeys.Add(typeof(TKey));
            _dictionaryOfTypes[typeof(TKey)] =typeof(TValue);
        }

        public static void RegisterSingltone<TKey, TValue>()
        {
            _dictionarySingltone[typeof(TKey)] = (TKey)Activator.CreateInstance(typeof(TValue));
            _allKeys.Add(typeof(TKey));
        }

        public static void RegisterSingltone<TKey,TValue>(TValue t)
        {
            _dictionarySingltone[typeof(TKey)] = t;
            _allKeys.Add(typeof(TKey));
            
        }

    }
}
