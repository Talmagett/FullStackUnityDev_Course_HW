using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public class Pool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T prefab;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private Transform container;
        
        [SerializeField] private int spawnOnStart;

        private readonly HashSet<T> _activeObjects = new();
        private readonly Queue<T> _pool = new();

        private void Awake()
        {
            for (var i = 0; i < spawnOnStart; i++)
            {
                var t = Rent();
                Return(t);
            }
        }
        
        public T Rent()
        {
            if (!_pool.TryDequeue(out var obj))
                obj = Instantiate(prefab, container);

            obj.transform.SetParent(worldTransform);
            _activeObjects.Add(obj);
            OnSpawned(obj);
            return obj;
        }

        public void Return(T obj)
        {
            if (_pool.Contains(obj))
                return;
            
            obj.transform.SetParent(container);
            _activeObjects.Remove(obj);
            OnDespawned(obj);
            _pool.Enqueue(obj);
        }

        protected virtual void OnSpawned(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected virtual void OnDespawned(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        public List<T> GetAllActiveObjects()
        {
            return _activeObjects.ToList();
        }
    }
}