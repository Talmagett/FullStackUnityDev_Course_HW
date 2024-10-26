using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletManager : MonoBehaviour
    {
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] private BulletPool bulletPool;

        private readonly List<Bullet> _cache = new();
        
        private void FixedUpdate()
        {
            _cache.Clear();
            _cache.AddRange(bulletPool.GetAllActiveObjects());

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                var bullet = _cache[i];
                if (!levelBounds.InBounds(bullet.transform.position)) bulletPool.Return(bullet);
            }
        }

        public void SpawnBullet(
            Vector2 position,
            Color color,
            int physicsLayer,
            int damage,
            bool isPlayer,
            Vector2 velocity
        )
        {
            var bullet = bulletPool.Rent();
            bullet.Init(position, color, physicsLayer, isPlayer, damage);
            bullet.SetVelocity(velocity);
        }
    }
}