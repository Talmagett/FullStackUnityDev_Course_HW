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

        [SerializeField] private Vector2Int spawnRange;
        
        private readonly List<Enemy> shootingEnemies=new();
        
        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(spawnRange.x, spawnRange.y));

                var enemy = enemyPool.Rent();

                var spawnPosition = RandomPoint(spawnPositions);
                enemy.transform.position = spawnPosition.position;

                var attackPosition = RandomPoint(attackPositions);
                enemy.SetDestination(attackPosition.position);

                enemy.OnDestroy += OnEnemyDestroy;
                if (shootingEnemies.Count >= 5) continue;
                enemy.OnFire += OnFire;
                shootingEnemies.Add(enemy);
            }
        }

        private void OnEnemyDestroy(Enemy enemy)
        {
            shootingEnemies.Remove(enemy);
            enemy.OnDestroy -= OnEnemyDestroy;
            enemy.OnFire -= OnFire;
        }

        private void OnFire(Enemy enemy)
        {
            Vector2 startPosition = enemy.spaceship.SpaceshipBulletData.FirePoint.position;
            var vector = (Vector2)gameData.Player.transform.position - startPosition;
            var direction = vector.normalized;
            enemy.spaceship.Fire(bulletSystem, direction);
        }

        private Transform RandomPoint(Transform[] points)
        {
            var index = Random.Range(0, points.Length);
            return points[index];
        }
    }
}