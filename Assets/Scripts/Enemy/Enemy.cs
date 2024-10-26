using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnDestroy;

        public delegate void FireHandler(Vector2 position, Vector2 direction);

        public event FireHandler OnFire;
        [field: SerializeField] public Spaceship spaceship { get; private set; }

        [SerializeField] private float countdown;

        [NonSerialized] private Spaceship _player;

        private Vector2 _destination;
        private float _currentTime;
        private bool _isPointReached;

        private void OnEnable()
        {
            spaceship.OnHealthEmpty += OnHealthEmpty;
        }

        private void OnDisable()
        {
            spaceship.OnHealthEmpty -= OnHealthEmpty;
        }

        public void SetTarget(Spaceship player)
        {
            _player = player;
        }

        private void OnHealthEmpty()
        {
            OnDestroy?.Invoke(this);
        }


        public void Activate()
        {
            gameObject.SetActive(true);
            Reset();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
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
                if (_player.Health <= 0)
                    return;

                _currentTime -= Time.fixedDeltaTime;
                if (_currentTime <= 0)
                {
                    Vector2 startPosition = spaceship.FirePoint.position;
                    var vector = (Vector2)_player.transform.position - startPosition;
                    var direction = vector.normalized;
                    OnFire?.Invoke(startPosition, direction);

                    _currentTime += countdown;
                }
            }
            else
            {
                //Move:
                var vector = _destination - (Vector2)transform.position;
                if (vector.magnitude <= 0.25f)
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