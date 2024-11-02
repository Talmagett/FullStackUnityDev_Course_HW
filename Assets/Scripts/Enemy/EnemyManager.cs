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

        [SerializeField] private BulletManager bulletManager;
        [SerializeField] private int maxShootingEnemies;
        
        [SerializeField] private Vector2Int spawnRange;
        
        private readonly List<Enemy> spawnedEnemies=new();
        
        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(spawnRange.x, spawnRange.y));
                if (spawnedEnemies.Count >= 5) continue;

                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            var enemy = enemyPool.Rent();

            enemy.spaceship.Init(bulletManager);
            enemy.Init(gameData.Player);

            var spawnPosition = RandomPoint(spawnPositions);
            enemy.transform.position = spawnPosition.position;

            var attackPosition = RandomPoint(attackPositions);
            enemy.SetDestination(attackPosition.position);
            
            enemy.OnDestroy += OnEnemyDestroy;
            spawnedEnemies.Add(enemy);
        }

        private void OnEnemyDestroy(Enemy enemy)
        {
            spawnedEnemies.Remove(enemy);
            enemy.OnDestroy -= OnEnemyDestroy;
        }
        
        
        private Transform RandomPoint(Transform[] points)
        {
            var index = Random.Range(0, points.Length);
            return points[index];
        }
    }
}