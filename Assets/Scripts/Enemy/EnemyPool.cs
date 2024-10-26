namespace ShootEmUp
{
    public class EnemyPool : Pool<Enemy>
    {
        protected override void OnSpawned(Enemy enemy)
        {
            enemy.spaceship.Activate();
            enemy.OnDestroy += Return;
        }

        protected override void OnDespawned(Enemy enemy)
        {
            enemy.spaceship.Deactivate();
            enemy.OnDestroy -= Return;
        }
    }
}