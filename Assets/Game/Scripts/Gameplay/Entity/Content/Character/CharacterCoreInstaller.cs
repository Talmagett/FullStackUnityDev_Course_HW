using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class CharacterCoreInstaller : SceneEntityInstaller
    {
        [SerializeField]
        private float _moveSpeed = 3;

        [SerializeField]
        private float _angularSpeed = 15;

        [SerializeField]
        private int _health = 3;

        [SerializeField]
        private GameObject _gameObject;

        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private SceneEntity _initialWeapon;

        [SerializeField]
        private TeamType _team;

        public override void Install(IEntity entity)
        {
            entity.AddCharacterTag();
            
            //Main
            entity.AddGameObject(_gameObject);
            entity.AddTransform(_transform);

            //Team
            entity.AddTeam(new ReactiveVariable<TeamType>(_team));
            entity.AddBehaviour<TeamLayerBehaviour>();

            //Life
            entity.AddDamageableTag();
            entity.AddHealth(new Health(_health, _health));
            entity.AddTakeDamageEvent(new BaseEvent<DamageArgs>());
            entity.AddTakeDeathEvent(new BaseEvent<DamageArgs>());

            //Movement:
            entity.AddMoveRequest(new BaseRequest<Vector3>());
            entity.AddMoveEvent(new BaseEvent<Vector3>());
            entity.AddBehaviour(new MoveBehaviour(
                condition: (_, _) => true,
                action: (direction, deltaTime) =>
                {
                    MoveUseCase.MoveTowards(entity, direction, deltaTime);
                    RotateUseCase.RotateTowards(entity, direction, deltaTime);
                }
            ));
            entity.AddMoveSpeed(new ReactiveVariable<float>(_moveSpeed));

            //Look:
            entity.AddLookRequest(new BaseRequest<Vector3>());
            entity.AddBehaviour(new LookBehaviour(
                condition: (_, _) => true,
                action: (direction, deltaTime) => RotateUseCase.RotateTowards(entity, direction, deltaTime)
            ));
            entity.AddRotateSpeed(new Const<float>(_angularSpeed));

            //Fire
            entity.AddFireRequest(new BaseRequest());
            entity.AddFireEvent(new BaseEvent());
            entity.AddFireCondition(new BaseFunction<bool>(() => entity.GetHealth().Exists() &&
                                                                 entity.GetWeapon().GetFireCondition().Invoke()));
            entity.AddBehaviour(new FireBehaviour(
                action: () => entity.GetWeapon().GetFireRequest().Invoke()
            ));

            //Weapon:
            entity.AddWeapon(_initialWeapon);
            entity.WhenInit(() => _initialWeapon.GetTeam().Value = _team);
        }
    }
}