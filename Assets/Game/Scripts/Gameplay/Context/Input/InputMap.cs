using UnityEngine;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "InputMap",
        menuName = "SampleGame/New InputMap"
    )]
    public sealed class InputMap : ScriptableObject
    {
        public KeyCode forward = KeyCode.W;
        public KeyCode back = KeyCode.S;
        public KeyCode left = KeyCode.A;
        public KeyCode right = KeyCode.D;

        public KeyCode fire = KeyCode.Space;
    }
}