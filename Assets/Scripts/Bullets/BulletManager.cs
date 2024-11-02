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
                if (!levelBounds.InBounds(bullet.Position)) bulletPool.Return(bullet);
            }
        }

        public void SpawnBullet(
            Vector2 position,
            Color color,
            int physicsLayer,
            int damage,
            Vector2 velocity
        )
        {
            var bullet = bulletPool.Rent();
            bullet.SetPosition(position);
            bullet.SetLayer(physicsLayer);
            bullet.SetVelocity(velocity);
            bullet.SetColor(color);
            bullet.SetDamage(damage);
        }
    }
}