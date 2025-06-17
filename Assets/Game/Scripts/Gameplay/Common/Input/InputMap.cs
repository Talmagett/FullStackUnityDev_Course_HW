using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Gameplay
{
    [CreateAssetMenu(
        fileName = "InputMap",
        menuName = "SampleGame/New InputMap"
    )]
    public sealed class InputMap : ScriptableObject
    {
        [field: SerializeField]
        public int Click;
        
        [field: SerializeField]
        public KeyCode Attack { get; private set; } = KeyCode.A;
        
        [field: SerializeField]
        public KeyCode Move { get; private set; } = KeyCode.M;

        [field: SerializeField]
        public KeyCode Patrol { get; private set; } = KeyCode.P;
        
        [field: SerializeField]
        public KeyCode Follow { get; private set; } = KeyCode.F;

        [field: SerializeField]
        public KeyCode Stop { get; private set; } = KeyCode.S;

        [field: SerializeField]
        public KeyCode Hold { get; private set; } = KeyCode.H;

        [field: SerializeField]
        public KeyCode Additive { get; private set; } = KeyCode.LeftShift;
    }
}