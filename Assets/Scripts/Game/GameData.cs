using UnityEngine;

namespace ShootEmUp
{
    public class GameData : MonoBehaviour
    {
        [field:SerializeField] public Spaceship Player { get; private set; }
    }
}