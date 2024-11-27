using System;
using UnityEngine;

namespace Game.Scripts.Player
{
    public interface IPlayerInput
    {
        event Action<Vector2Int> OnMoved;
    }
}