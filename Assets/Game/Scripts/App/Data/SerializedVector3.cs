using System;
using System.Collections.Generic;

namespace SampleGame.App
{
    [Serializable]
    public struct SerializableVector3
    {
        public float x, y, z;
        public SerializableVector3(float x, float y, float z)
        {
            this.x = x; this.y = y; this.z = z;
        }
    }
}