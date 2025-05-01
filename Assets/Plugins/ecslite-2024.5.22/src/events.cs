using System;
using System.Collections.Generic;

namespace Leopotam.EcsLite
{
    public sealed partial class EcsWorld
    {
        private readonly Dictionary<Type, IEcsEvent> _events = new();

        public EcsEvent<T> GetEvent<T>() where T : struct
        {
            Type eventType = typeof(T);

            if (_events.TryGetValue(eventType, out IEcsEvent rawPool))
                return (EcsEvent<T>) rawPool;

            EcsEvent<T> pool = new EcsEvent<T>();
            _events.Add(eventType, pool);
            return pool;
        }
    }

    public interface IEcsEvent
    {
    }
    
    public sealed class EcsEvent<T> : IEcsEvent
        where T : struct
    {
        public int Count => _count;

        private int _count;
        private T[] _items;

        public EcsEvent()
        {
            _items = new T[1];
            _count = 0;
        }

        public void Fire(in T data)
        {
            int capacity = _items.Length;
            if (_count == capacity)
                Array.Resize(ref _items, capacity * 2);

            _items[_count++] = data;
        }

        public bool Consume(out T data)
        {
            if (_count == 0)
            {
                data = default;
                return false;
            }

            data = _items[--_count];
            return true;
        }

        public void Clear()
        {
            _count = 0;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IDisposable
        {
            public T Current => _current;

            private EcsEvent<T> _event;
            private int _index;
            private T _current;

            public Enumerator(EcsEvent<T> @event)
            {
                _event = @event;
                _index = -1;
                _current = default;
            }

            public bool MoveNext()
            {
                if (_index + 1 == _event._count)
                    return false;

                _current = _event._items[++_index];
                return true;
            }

            public void Dispose()
            {
                _event = null;
            }
        }
    }

    public sealed class ClearEventSystem<T> : IEcsRunSystem where T : struct
    {
        private readonly EcsEvent<T> _event;

        public ClearEventSystem(EcsWorld world)
        {
            _event = world.GetEvent<T>();
        }

        public void Run(IEcsSystems systems)
        {
            _event.Clear();
        }
    }
}