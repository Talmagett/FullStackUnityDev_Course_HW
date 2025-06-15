using System.Collections.Generic;
using SampleGame.App;
using SampleGame.Common;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class DestinationPoint : MonoBehaviour, ISaveLoadData
    {
        ///Variable
        [field: SerializeField]
        public Vector3 Value { get; set; }

        public void LoadData(Dictionary<string, string> properties)
        {
            if (properties.ContainsKey("DestinationPoint"))
            {
                var saveData = JsonUtility.FromJson<SerializedVector3>(properties["DestinationPoint"]);
                Value = saveData;
            }
            else
            {
                Value = Vector3.zero; // Default value if not found
            }        
        }

        Dictionary<string, string> ISaveLoadData.SaveData()
        {
            var serializedData = new SerializedVector3(Value);
            var data = new Dictionary<string, string>
            {
                { "DestinationPoint", JsonUtility.ToJson(serializedData)}
            };
            return data;
        }

        public struct SaveData
        {
            public Vector3 value;
        }
    }
}