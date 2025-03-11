using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Components
{
    public class PatrolComponent : MonoBehaviour
    {
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private Transform movingTransform;

        [SerializeField] private Transform[] wayPoints;
        [SerializeField] private int currentPoint;
        [SerializeField] private float stoppingDistance;
        
        private void Update()
        {
            Patrol();
        }

        private void Patrol()
        {
            if (wayPoints.Length == 0)
                return;

            Vector3 directionToWaypoint = wayPoints[currentPoint].position - movingTransform.position;

            if (directionToWaypoint.magnitude > stoppingDistance)
            {
                moveComponent.SetDirection(directionToWaypoint.normalized);
            }
            else
            {
                currentPoint = (currentPoint + 1) % wayPoints.Length;
            }
        }
    }
}