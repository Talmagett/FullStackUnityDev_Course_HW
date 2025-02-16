using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.App.Audio.Sound
{
    [CreateAssetMenu(
        fileName = "SoundCatalog",
        menuName = "Game/App/New SoundCatalog"
    )]
    public sealed class SoundCatalog : ScriptableObject
    {
        [SerializeField]
        private Sound[] _sounds;

        public AudioClip GetSound(SoundName name)
        {
            for (int i = 0, count = _sounds.Length; i < count; i++)
            {
                Sound sound = _sounds[i];
                if (sound.name == name)
                    return sound.clip;
            }

            throw new KeyNotFoundException($"Sound with name {name} is not found!");
        } 
        
        [Serializable]
        private struct Sound
        {
            public SoundName name;
            public AudioClip clip;
        }
    }
}