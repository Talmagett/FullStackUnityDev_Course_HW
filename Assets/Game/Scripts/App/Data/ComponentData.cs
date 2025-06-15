using System;
using System.Collections.Generic;
using System.Numerics;

namespace SampleGame.App
{
    [Serializable]
    public struct ComponentData
    {
        public string type;
        public Dictionary<string, string> properties;
    }
}