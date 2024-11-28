using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Player
{
    public class PlayerInput : IPlayerInput, ITickable
    {
        public event Action<Vector2Int> OnMoved;

        public void Tick()
        {
            var horizontal = (int)Input.GetAxisRaw("Horizontal");
            var vertical = (int)Input.GetAxisRaw("Vertical");
            var direction = new Vector2Int(horizontal, vertical);
            
            OnMoved?.Invoke(direction);
        }
    }
}