using System.Collections.Generic;
using SampleGame.App;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Health : MonoBehaviour, ISaveLoadData
    {
        ///Variable
        [field: SerializeField]
        public int Current { get; set; } = 50;

        ///Const
        [field: SerializeField]
        public int Max { get; private set; } = 100;

        public void LoadData(Dictionary<string, string> properties)
        {
            Current = properties.ContainsKey("CurrentHealth") ? int.Parse(properties["CurrentHealth"]) : Max;
        }

        public Dictionary<string, string> SaveData()
        {
            var data = new Dictionary<string, string>
            {
                { "CurrentHealth", Current.ToString() }
            };
            return data;
        }
    }
}