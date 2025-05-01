using System;
using System.Collections.Generic;

namespace Leopotam.EcsLite
{
    public sealed partial class EcsWorld
    {
        private readonly Dictionary<Type, object> _singletons = new();

        public T GetSingleton<T>() where T : class
        {
            if (_singletons.TryGetValue(typeof(T), out object singleton))
                return (T) singleton;

            throw new Exception($"Singleton of type {typeof(T).Name} is not added!");
        }
        public void AddSingleton<T>(T singleton) where T : class
        {
            if (_singletons.ContainsKey(typeof(T)))
                throw new Exception($"Singleton of type {typeof(T).Name} is already added!");

            _singletons.Add(typeof(T), singleton);
        }
        public bool RemoveSingleton<T>()
        {
            return _singletons.Remove(typeof(T));
        }
        
        public bool HasSingleton<T>()
        {
            return _singletons.ContainsKey(typeof(T));
        }
    }
}