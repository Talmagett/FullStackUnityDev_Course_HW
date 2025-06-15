using System.Collections.Generic;
using SampleGame.App;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Countdown : MonoBehaviour, ISaveLoadData
    {
        ///Variable
        [field: SerializeField]
        public float Current { get; set; }

        ///Const
        [field: SerializeField]
        public float Duration { get; private set; }
        public Dictionary<string, string> SaveData()
        {
            var data = new Dictionary<string, string>
            {
                { "Current", Current.ToString() }
            };
            return data;
        }
        public void LoadData(Dictionary<string, string> properties)
        {
            Current = properties.ContainsKey("Current") ? float.Parse(properties["Current"]) : 0f;
        }
    }
}