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
            if (_dictionarySingltone.ContainsKey(typeof(TKey)))
            {
                return (TKey)_dictionarySingltone[typeof(TKey)];
            }

            if(_dictionaryTypeParameter.ContainsKey(typeof(TKey)))
            {
                return (TKey)_dictionaryTypeParameter[typeof(TKey)];
            }

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
                    else
                        if (p.ParameterType.IsInterface)
                    {
                        throw new Exception("Register this interface: " + p.ParameterType);
                    }
                    else
                    {
                        throw new Exception("This dependency cannot be resolved");
                    }
                }

                return (TKey)Activator.CreateInstance(_dictionaryOfTypes[typeof(TKey)], parameters.ToArray());
            }

            if (_dictionaryOfTypes.ContainsKey(typeof(TKey)))
            {
                return (TKey)Activator.CreateInstance(_dictionaryOfTypes[typeof(TKey)]);
            }
                
            
            return default(TKey);
        }

        public static void Register<TKey, TValue>(TValue t) where TValue: TKey
        {
            if(!typeof(TKey).IsInterface || !typeof(TValue).IsClass)
            {
                throw new Exception("Try another pair to register");
            }
            RemoveOldPairs<TKey>();
            _allKeys.Add(typeof(TKey));
            _dictionaryTypeParameter[typeof(TKey)] = t;
        }

        public static void Register<TKey, TValue>() where TValue : TKey
        {
            if (!typeof(TKey).IsInterface || !typeof(TValue).IsClass)
            {
                throw new Exception("Try another pair to register");
            }
            RemoveOldPairs<TKey>();
            _allKeys.Add(typeof(TKey));
            _dictionaryOfTypes[typeof(TKey)] = typeof(TValue);
        }

        public static void RegisterSingltone<TKey, TValue>() where TValue : TKey
        {
            if (!typeof(TKey).IsInterface || !typeof(TValue).IsClass)
            {
                throw new Exception("Try another pair to register");
            }
            RemoveOldPairs<TKey>();
            _dictionarySingltone[typeof(TKey)] = (TKey)Activator.CreateInstance(typeof(TValue));
            _allKeys.Add(typeof(TKey));
        }

        public static void RegisterSingltone<TKey, TValue>(TValue t) where TValue : TKey
        {
            if (!typeof(TKey).IsInterface || !typeof(TValue).IsClass)
            {
                throw new Exception("Try another pair to register");
            }
            RemoveOldPairs<TKey>();
            _dictionarySingltone[typeof(TKey)] = t;
            _allKeys.Add(typeof(TKey));
        }

        private static void RemoveOldPairs<TKey>()
        {
            if (_dictionaryOfTypes.ContainsKey(typeof(TKey)))
            {
                _dictionaryOfTypes.Remove(typeof(TKey));
            }

            if (_dictionarySingltone.ContainsKey(typeof(TKey)))
            {
                _dictionarySingltone.Remove(typeof(TKey));
            }

            if (_dictionaryTypeParameter.ContainsKey(typeof(TKey)))
            {
                _dictionaryTypeParameter.Remove(typeof(TKey));
            }
        }
    }
}
