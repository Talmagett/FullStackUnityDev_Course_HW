// using UnityEngine;
//
// namespace SampleGame
// {
//     public class Tower : MonoBehaviour
//     {
//         [SerializeField] private LifeComponent _lifeComponent;
//         [SerializeField] private RotateComponent _rotateComponent;
//         [SerializeField] private ShootComponent _shootComponent;
//         [SerializeField] private ReloadComponent _reloadComponent;
//         [SerializeField] private DetectTargetComponent _detectTargetComponent;
//
//         private void Awake()
//         {
//             _rotateComponent.AddCondition(_lifeComponent.IsAlive);    
//             _shootComponent.AddCondition(_lifeComponent.IsAlive);    
//             _shootComponent.AddCondition(_detectTargetComponent.HasTarget);
//             _shootComponent.AddCondition(_reloadComponent.IsReady);
//         }
//
//         private void OnEnable()
//         {
//             _shootComponent.OnFire += _reloadComponent.Reload;
//             _lifeComponent.OnEmpty += this.OnHealthEmpty;
//         }
//
//         private void OnDisable()
//         {
//             _shootComponent.OnFire -= _reloadComponent.Reload;
//             _lifeComponent.OnEmpty -= this.OnHealthEmpty;
//         }
//
//         private void Update()
//         {
//             _rotateComponent.SetDirection(_detectTargetComponent.GetTarget());
//             _shootComponent.Shoot();
//         }
//
//         private void OnHealthEmpty()
//         {
//             Destroy(this.gameObject);
//         }
//     }
// }