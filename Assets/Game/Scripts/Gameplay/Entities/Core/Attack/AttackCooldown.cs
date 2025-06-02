using System;

namespace SampleGame
{
    [Serializable]
    public struct AttackCooldown
    {
        public float current;
        public float max;
    }
}