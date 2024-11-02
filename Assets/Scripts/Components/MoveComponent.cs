using UnityEngine;

namespace Components
{
    [System.Serializable]
    public class MoveComponent
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private float speed = 5.0f;
        
        public void Move(Vector2 moveDirection)
        {
            Vector2 moveStep = moveDirection * (Time.fixedDeltaTime * speed);
            var nextPosition = rigidbody2D.position + moveStep;
            rigidbody2D.MovePosition(nextPosition);
        }
    }
}