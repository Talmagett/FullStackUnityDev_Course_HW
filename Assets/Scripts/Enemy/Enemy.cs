using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnDestroy;
        public event Action<Enemy> OnFire;
        
        [field: SerializeField] public Spaceship spaceship { get; private set; }

        [SerializeField] private float countdown;

        private Vector2 _destination;
        private float _currentTime;
        private bool _isPointReached;
        private const float _reachDistance = 0.25f;
        
        private void OnEnable()
        {
            spaceship.OnHealthEmpty += OnHealthEmpty;
        }

        private void OnDisable()
        {
            spaceship.OnHealthEmpty -= OnHealthEmpty;
        }

        private void OnHealthEmpty()
        {
            OnDestroy?.Invoke(this);
        }

        public void Reset()
        {
            _currentTime = countdown;
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isPointReached = false;
        }

        private void FixedUpdate()
        {
            if (_isPointReached)
            {
                //Attack:

                _currentTime -= Time.fixedDeltaTime;
                if (_currentTime <= 0)
                {
                    OnFire?.Invoke(this);
                    _currentTime += countdown;
                }
            }
            else
            {
                //Move:
                var vector = _destination - (Vector2)transform.position;
                if (vector.magnitude <= _reachDistance)
                {
                    _isPointReached = true;
                    return;
                }

                var moveDirection = vector.normalized;
                spaceship.Move(moveDirection);
            }
        }
    }
}