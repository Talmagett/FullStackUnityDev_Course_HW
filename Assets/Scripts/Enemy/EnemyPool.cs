namespace ShootEmUp
{
    public class EnemyPool : Pool<Enemy>
    {
        protected override void OnSpawned(Enemy obj)
        {
            obj.OnDestroy += Return;
        }

        protected override void OnDespawned(Enemy obj)
        {
            obj.OnDestroy -= Return;
        }
    }
}