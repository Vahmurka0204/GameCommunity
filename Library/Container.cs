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
        private static Dictionary<Type, Type> _dictionaryOfTypes;
        private static Dictionary<Type, object> _dictionaryTypeParameter;
        private static Dictionary<Type, object> _dictionarySingltone;
        private static HashSet<Type> _allKeys;

        static Container()
        {
            _dictionaryOfTypes = new Dictionary<Type, Type>();
            _dictionaryTypeParameter = new Dictionary<Type, object>();
            _dictionarySingltone = new Dictionary<Type, object>();
            _allKeys = new HashSet<Type>();
        }

        public static T Cast<T>(object o)
        {
            //T result = o as T;
            T result = (T)o;
            var t = typeof(T);

            return (T)result;
        }

        public static TKey Resolve<TKey>()
        {
            if (!_dictionaryOfTypes.ContainsKey(typeof(TKey)))
            {
                return default(TKey);
            }

            ConstructorInfo[] ctorInfo = _dictionaryOfTypes[typeof(TKey)].GetConstructors();

            foreach (ConstructorInfo c in ctorInfo)
            {
                ParameterInfo[] parameterInfo = c.GetParameters();

                if (parameterInfo.Length == 0)
                {
                    break;
                }

                var parameters = new List<object>(parameterInfo.Length);

                foreach (ParameterInfo p in parameterInfo)
                {
                    if (_allKeys.Contains(p.ParameterType))
                    {
                        var resolveMethod = typeof(Container).GetMethod("Resolve").MakeGenericMethod(p.ParameterType);
                        object returnValue = resolveMethod.Invoke(null, null);

                        var castMethod = typeof(Container).GetMethod("Cast").MakeGenericMethod(p.ParameterType);
                        object castedValue = castMethod.Invoke(null, new object[] { returnValue });

                        parameters.Add(castedValue);
                    }
                }

                return (TKey)Activator.CreateInstance(_dictionaryOfTypes[typeof(TKey)], parameters);
            }

            if (!_dictionarySingltone.ContainsKey(typeof(TKey)))
            {
                if (_dictionaryOfTypes.ContainsKey(typeof(TKey)))
                {
                    return (TKey)Activator.CreateInstance(_dictionaryOfTypes[typeof(TKey)]);
                }
                else if (_dictionaryTypeParameter.ContainsKey(typeof(TKey)))
                {
                    return (TKey)_dictionaryTypeParameter[typeof(TKey)];
                }
            }

            return (TKey)_dictionarySingltone[typeof(TKey)];
        }

        public static void Register<TKey, TValue>(TValue t) where TValue: TKey
        {
            _allKeys.Add(typeof(TKey));
            _dictionaryTypeParameter[typeof(TKey)] = t;
        }

        public static void Register<TKey, TValue>() where TValue : TKey
        {
            _allKeys.Add(typeof(TKey));
            _dictionaryOfTypes[typeof(TKey)] = typeof(TValue);
        }

        public static void RegisterSingltone<TKey, TValue>() where TValue : TKey
        {
            _dictionarySingltone[typeof(TKey)] = (TKey)Activator.CreateInstance(typeof(TValue));
            _allKeys.Add(typeof(TKey));
        }

        public static void RegisterSingltone<TKey, TValue>(TValue t) where TValue : TKey
        {
            _dictionarySingltone[typeof(TKey)] = t;
            _allKeys.Add(typeof(TKey));
        }
    }
}
