using System.Collections.Generic;
using Modules.Entities;
using SampleGame.App;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class TargetObject : MonoBehaviour, ISaveLoadData
    {
        ///Variable
        [field: SerializeField]
        public Entity Value { get; set; }

        public void LoadData(Dictionary<string, string> properties)
        {
            if (!properties.ContainsKey("TargetObjectId"))
            {
                Value = null;
                return;
            }
            var targetId = int.Parse(properties["TargetObjectId"]);
            if (targetId == -1)
            {
                Value = null;
                return;
            }
            if (FindAnyObjectByType<EntityWorld>().TryGet(targetId, out var targetEntity))
                Value = targetEntity;
        }

        public Dictionary<string, string> SaveData()
        {
            var data = new Dictionary<string, string>
            {
                { "TargetObjectId", Value != null ? Value.Id.ToString() : "-1" }
            };
            return data;
        }
    }
}