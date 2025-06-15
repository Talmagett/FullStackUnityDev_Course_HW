using System.Collections.Generic;
using SampleGame.App;
using SampleGame.Common;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class ResourceBag : MonoBehaviour, ISaveLoadData
    {
        ///Variable
        [field: SerializeField]
        public ResourceType Type { get; set; }

        ///Variable
        [field: SerializeField]
        public int Current { get; set; }

        ///Const
        [field: SerializeField]
        public int Capacity { get; set; }

        public void LoadData(Dictionary<string, string> properties)
        {
            Type = properties.ContainsKey("ResourceBagType") ? (ResourceType)int.Parse(properties["ResourceBagType"]) : ResourceType.Minerals;
            Current = properties.ContainsKey("ResourceBagCurrent") ? int.Parse(properties["ResourceBagCurrent"]) : 0;
        }

        public Dictionary<string, string> SaveData()
        {
            var data = new Dictionary<string, string>
            {
                { "ResourceBagType", ((int)Type).ToString() },
                { "ResourceBagCurrent", Current.ToString() },
            };
            return data;
        }
    }
}