using System;
using System.Collections.Generic;

namespace SampleGame.App
{
    [Serializable]
    public struct EntityWorldData
    {
        public int version;
        public List<EntityData> entities;
    }
}