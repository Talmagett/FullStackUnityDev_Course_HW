using System;
using System.Collections.Generic;
using Modules.Entities;
using UnityEngine;

namespace SampleGame.App
{
    [Serializable]
    public struct EntityData
    {
        public int id;
        public string name;
        public EntityType entityType;
        public SerializableVector3 position;
        public SerializableVector3 rotation;
        public List<ComponentData> components;
    }
    
}