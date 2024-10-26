using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPositions;

        [SerializeField] private Transform[] attackPositions;

        [SerializeField] private GameData gameData;
        [SerializeField] private EnemyPool enemyPool;

        [SerializeField] private BulletManager bulletSystem;

        private readonly List<Enemy> shootingEnemies=new();
        
        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1, 2));

                var enemy = enemyPool.Rent();

                var spawnPosition = RandomPoint(spawnPositions);
                enemy.transform.position = spawnPosition.position;

                var attackPosition = RandomPoint(attackPositions);
                enemy.SetDestination(attackPosition.position);

                enemy.SetTarget(gameData.Player);
                enemy.OnDestroy += OnDestroy;
                if (shootingEnemies.Count >= 5) continue;
                enemy.OnFire += OnFire;
                shootingEnemies.Add(enemy);
            }
        }

        private void OnDestroy(Enemy enemy)
        {
            shootingEnemies.Remove(enemy);
            enemy.OnDestroy -= OnDestroy;
            enemy.OnFire -= OnFire;
        }

        private void OnFire(Vector2 position, Vector2 direction)
        {
            bulletSystem.SpawnBullet(
                position,
                Color.red,
                (int)PhysicsLayer.ENEMY_BULLET,
                1,
                false,
                direction * 2
            );
        }

        private Transform RandomPoint(Transform[] points)
        {
            var index = Random.Range(0, points.Length);
            return points[index];
        }
    }
}