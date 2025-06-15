using System.Collections.Generic;
using System.Linq;
using Modules.Entities;
using SampleGame.App;
using UnityEngine;
using Zenject;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class ProductionOrder : MonoBehaviour, ISaveLoadData
    {
        ///Variable
        [SerializeField]
        private List<EntityConfig> _queue;

        public IReadOnlyList<EntityConfig> Queue
        {
            get { return _queue; }
            set { _queue = new List<EntityConfig>(value); }
        }
        private EntityCatalog _entityCatalog;
        [Inject]
        public void Ctor(EntityCatalog entityCatalog)
        {
            _entityCatalog = entityCatalog;
        }
        public void LoadData(Dictionary<string, string> properties)
        {
            if (_entityCatalog == null)
            {
                Debug.LogError("EntityCatalog is not injected into ProductionOrder.");
                return;
            }
            if (properties.ContainsKey("ProductionOrder"))
            {
                var orderData = JsonUtility.FromJson<OrderData>(properties["ProductionOrder"]);
                foreach (var name in orderData.queue)
                {
                    if (_entityCatalog.FindConfig(name, out var config))
                        _queue.Add(config);
                }
            }
        }

        public Dictionary<string, string> SaveData()
        {
            OrderData orderData = new OrderData
            {
                queue = Queue.Select(t => t.Name).ToArray()
            };
            var data = new Dictionary<string, string>
            {
                { "ProductionOrder", JsonUtility.ToJson(orderData) }
            };
            return data;
        }
        public struct OrderData
        {
            public string[] queue;
        }
    }
}