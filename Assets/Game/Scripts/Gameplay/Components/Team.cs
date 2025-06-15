using System.Collections.Generic;
using SampleGame.App;
using SampleGame.Common;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Team : MonoBehaviour, ISaveLoadData
    {
        ///Variable
        [field: SerializeField]
        public TeamType Type { get; set; }

        public void LoadData(Dictionary<string, string> properties)
        {
            Type = properties.ContainsKey("TeamType") ? (TeamType)int.Parse(properties["TeamType"]) : TeamType.NEUTRAL;
        }

        public Dictionary<string, string> SaveData()
        {
            var data = new Dictionary<string, string>
            {
                { "TeamType", ((int)Type).ToString() }
            };
            return data;
        }
    }
}