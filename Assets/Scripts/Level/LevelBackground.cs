using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        //19,-38, 5
        [SerializeField] private float startPositionY;
        [SerializeField] private float endPositionY;
        [SerializeField] private float movingSpeedY;

        private float _positionX;
        private float _positionZ;

        private void Awake()
        {
            var position = transform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        private void FixedUpdate()
        {
            if (transform.position.y <= endPositionY)
                transform.position = new Vector3(_positionX, startPositionY, _positionZ);

            transform.position -= new Vector3(_positionX, movingSpeedY * Time.fixedDeltaTime, _positionZ
            );
        }
    }
}