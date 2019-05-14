using System;
using System.Collections.Generic;
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

        public static void Clean()
        {
            _dictionaryOfTypes.Clear();
            _dictionarySingltone.Clear();
            _dictionaryTypeParameter.Clear();
            _allKeys.Clear();
        }

        public static T Cast<T>(object o)
        {
            T result = (T)o;
            var t = typeof(T);

            return (T)result;
        }

        public static TKey Resolve<TKey>()
        {
            if (!_allKeys.Contains(typeof(TKey)))
            {
                throw new ContainerException("Register this type: " + typeof(TKey));
            }

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

            if (!HasEmptyConstructor(ctorInfo))
            {
                var suitableConstructor = FindSuitableConstructor(ctorInfo, typeof(TKey));
                if (suitableConstructor == null)
                {
                    throw new ContainerException("Cannot resolve");
                }
                ParameterInfo[] parameterInfo = suitableConstructor.GetParameters();
                var parameters = new List<object>(parameterInfo.Length);

                foreach (ParameterInfo p in parameterInfo)
                {
                    var resolveMethod = typeof(Container).GetMethod("Resolve").MakeGenericMethod(p.ParameterType);
                    object returnValue = resolveMethod.Invoke(null, null);
                    var castMethod = typeof(Container).GetMethod("Cast").MakeGenericMethod(p.ParameterType);
                    object castedValue = castMethod.Invoke(null, new object[] { returnValue });
                    parameters.Add(castedValue);
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
                throw new ContainerException("Try another pair to register");
            }

            RemoveOldPairs<TKey>();
            _allKeys.Add(typeof(TKey));
            _dictionaryTypeParameter[typeof(TKey)] = t;
        }

        public static void Register<TKey, TValue>() where TValue : TKey
        {
            if (!typeof(TKey).IsInterface || !typeof(TValue).IsClass)
            {
                throw new ContainerException("Try another pair to register");
            }

            RemoveOldPairs<TKey>();
            _allKeys.Add(typeof(TKey));
            _dictionaryOfTypes[typeof(TKey)] = typeof(TValue);
        }

        public static void RegisterSingltone<TKey, TValue>() where TValue : TKey
        {
            if (!typeof(TKey).IsInterface || !typeof(TValue).IsClass)
            {
                throw new ContainerException("Try another pair to register");
            }

            RemoveOldPairs<TKey>();
            _dictionarySingltone[typeof(TKey)] = (TKey)Activator.CreateInstance(typeof(TValue));
            _allKeys.Add(typeof(TKey));
        }

       /* public static void RegisterSingltone<TKey, TValue>(TValue t) where TValue : TKey
        {
            if (!typeof(TKey).IsInterface || !typeof(TValue).IsClass)
            {
                throw new ContainerException("Try another pair to register");
            }

            RemoveOldPairs<TKey>();
            _dictionarySingltone[typeof(TKey)] = t;
            _allKeys.Add(typeof(TKey));
        }*/

        private static bool HasEmptyConstructor(ConstructorInfo[] _ctorInfo)
        {
            foreach (ConstructorInfo c in _ctorInfo)
            {
                ParameterInfo[] parameterInfo = c.GetParameters();

                if (parameterInfo.Length == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private static ConstructorInfo FindSuitableConstructor(ConstructorInfo[] _ctorInfo, Type type)
        {
            foreach (ConstructorInfo c in _ctorInfo)
            {
                ParameterInfo[] parameterInfo = c.GetParameters();
                var parameters = new List<object>(parameterInfo.Length);//delete
                bool flag = true;
                foreach (ParameterInfo p in parameterInfo)
                {
                    if (!_allKeys.Contains(p.ParameterType) || HasCycle(p.ParameterType, type))
                    {
                        flag = false;
                        //break;
                    }
                }
                if(flag)
                {
                    return c;
                }
                
            }

            return null;
        }

        private static bool HasCycle(Type t, Type firstType)
        {
            HashSet<Type> setTypes = new HashSet<Type>();
            bool flag=false;
            hasCycle(t, setTypes, flag, firstType);
            return flag;
        }

        private static void hasCycle(Type t, HashSet<Type> setTypes, bool flag, Type firstType)
        {
            if (setTypes.Contains(t) || t == firstType)
            {
                flag = true;
                return;
            }
            setTypes.Add(t);
            //
            ConstructorInfo[] ctorInfo = t.GetConstructors();//
            //
            if (!HasEmptyConstructor(ctorInfo))
            {
                foreach (ConstructorInfo ctor in ctorInfo)
                {
                    ParameterInfo[] parameterInfo = ctor.GetParameters();

                    foreach (ParameterInfo p in parameterInfo)
                    {
                        if (_allKeys.Contains(p.ParameterType))
                        {
                            hasCycle(p.ParameterType, setTypes, flag, firstType);
                        }
                        else
                        {
                            /* setTypes.Clear();
                             break;*/
                            flag = true;
                            return;
                        }
                    }
                }

            }
            else return;
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
