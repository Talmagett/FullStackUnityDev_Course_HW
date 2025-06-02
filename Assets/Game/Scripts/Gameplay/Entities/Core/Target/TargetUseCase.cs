using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Burst;
using Unity.Mathematics;

namespace SampleGame
{
    [BurstCompile]
    public readonly struct TargetUseCase
    {
        [BurstCompile]
        public static void MoveStep(
            ref Position position,
            in MoveDirection moveDirection,
            in MoveSpeed moveSpeed,
            in float deltaTime
        )
        {
            position.value += moveDirection.value * moveSpeed.value * deltaTime;
        }
        
        public static int FindClosestEnemy(
            in float3 myPosition,
            TeamType myTeam,
            EcsFilter filter,
            EcsPool<Position> positionsPool,
            EcsPool<TeamType> teamPool,
            EcsPool<Health> healthPool)
        {
            int closest = -1;
            float minDist = float.MaxValue;

            foreach (var other in filter)
            {
                ref var othTeam = ref teamPool.Get(other);
                if (othTeam == myTeam) continue;
                if (!healthPool.Has(other)) continue;
                
                ref var otherHealth = ref healthPool.Get(other);
                if (otherHealth.current <= 0) continue;

                ref var othPos = ref positionsPool.Get(other);
                float distSqr = math.distancesq(othPos.value , myPosition);
                if (distSqr < minDist)
                {
                    minDist = distSqr;
                    closest = other;
                }
            }
            return closest;
        }
    }
}