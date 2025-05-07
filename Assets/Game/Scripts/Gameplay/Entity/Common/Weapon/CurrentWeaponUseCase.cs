using Atomic.Elements;
using Atomic.Entities;
using Modules.Gameplay;
using SampleGame;
using UnityEngine;

namespace Game.Gameplay
{
    public static class CurrentWeaponUseCase
    {
        public static bool AddClips(in IEntity character, in int clips)
        {
            IEntity weapon = character.GetCurrentWeapon().Value;
            if (weapon == null)
                return false;

            if (!weapon.TryGetAmmo(out Ammo ammo))
                return false;

            ammo.Add(clips);
            return true;
        }
    }
}