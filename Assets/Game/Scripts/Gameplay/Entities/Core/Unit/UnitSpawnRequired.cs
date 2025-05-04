using System;

namespace SampleGame
{
    [Serializable]
    public struct UnitSpawnRequired
    {
        public bool value;
        public UnitSpawnType type;
    }
    public enum UnitSpawnType
    {
        None,
        Base,
        Archer,
        Swordman,
    }
}