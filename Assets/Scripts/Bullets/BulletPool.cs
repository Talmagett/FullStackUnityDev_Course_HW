namespace ShootEmUp
{
    public sealed class BulletPool : Pool<Bullet>
    {
        protected override void OnSpawned(Bullet obj)
        {
            obj.OnDestroy += Return;
        }

        protected override void OnDespawned(Bullet obj)
        {
            obj.OnDestroy -= Return;
        }
    }
}