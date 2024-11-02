using ShootEmUp;
using UnityEngine;

namespace Components
{
    [System.Serializable]
    public class FireComponent
    {
        [SerializeField] private Transform _firePoint ;
        [SerializeField] private int _bulletDamage;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private Color _bulletColor;
        [SerializeField] private PhysicsLayer _layer;

        private BulletManager _bulletManager;

        public void Init(BulletManager bulletManager)
        {
            _bulletManager = bulletManager;
        }
        
        public void Fire(Vector3 fireDirection)
        {
            _bulletManager.SpawnBullet(
                _firePoint.position,
                _bulletColor,
                (int) _layer,
                _bulletDamage,
                fireDirection * _bulletSpeed
            );
        }
    }
}