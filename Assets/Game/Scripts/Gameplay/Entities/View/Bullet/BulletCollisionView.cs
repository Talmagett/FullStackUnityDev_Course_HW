using Leopotam.EcsLite;
using UnityEngine;

namespace SampleGame
{
    public sealed class BulletCollisionView : MonoBehaviour
    {
        [SerializeField]
        private EcsView _view;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out EcsView target)) 
                return;
            
            IEcsSystems systems = EcsAdmin.Systems;
            Debug.Log($"Bullet Collision {name} {other.name}");

            systems.GetWorld().GetEvent<BulletCollisionRequest>().Fire(new BulletCollisionRequest
            {
                bullet = _view.GetPackedEntity(),
                target = target.GetPackedEntity()
            });
        }
    }
}