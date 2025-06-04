using System.Linq;
using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{
    public sealed class SpawnPointsInstaller : EcsComponentInstaller<SpawnPoints>
    {
        [SerializeField] private Transform[] _spawnPoints;

        protected override SpawnPoints GetValue()
        {
            SpawnPoints spawnPoints = new SpawnPoints();
            spawnPoints.values = new float3[_spawnPoints.Length];
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                spawnPoints.values[i] = _spawnPoints[i].localPosition;
            }
            return spawnPoints;
        }
    }
}