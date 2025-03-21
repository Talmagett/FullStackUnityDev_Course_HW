using Atomic.Entities;
using Game.Scripts.Gameplay.Context;
using UnityEngine;

namespace SampleGame
{
    public static class FireBulletUseCase
    {
        public static IEntity FireBullet(in IEntity weapon, in IGameContext context)
        {
            Transform firePoint = weapon.GetFirePoint();
            return SpawnBulletUseCase.SpawnBullet(context, firePoint.position, firePoint.rotation);
        }
    }
}