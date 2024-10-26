namespace ShootEmUp
{
    public sealed class BulletPool : Pool<Bullet>
    {
        protected override void OnSpawned(Bullet enemy)
        {
            enemy.OnDestroy += Return;
        }

        protected override void OnDespawned(Bullet enemy)
        {
            enemy.OnDestroy -= Return;
        }
    }
}