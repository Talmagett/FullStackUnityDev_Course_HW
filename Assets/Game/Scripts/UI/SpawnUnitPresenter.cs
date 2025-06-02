using System;
using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    [RequireComponent(typeof(Button))]
    public sealed class SpawnUnitPresenter : MonoBehaviour
    {
        private Button _spawnUnitButton;

        [SerializeField]
        private TeamType _teamType;

        [SerializeField]
        private UnitSpawnType _unitSpawnType;

        private void Awake()
        {
            _spawnUnitButton = GetComponent<Button>();
        }

        void OnEnable()
        {
            _spawnUnitButton.onClick.AddListener(OnSpawnUnitButtonClick);
        }
        void OnDisable()
        {
            _spawnUnitButton.onClick.RemoveListener(OnSpawnUnitButtonClick);
        }
        
        private void OnSpawnUnitButtonClick()
        {
            var world = EcsAdmin.Systems.GetWorld();
            var filter = world.Filter<BaseTag>().End();
            var unitSpawnRequiredPool = world.GetPool<UnitSpawnRequired>();
            var team = world.GetPool<TeamType>();
            foreach (int entity in filter)
            {
                if(team.Get(entity) != _teamType)
                    continue;
                ref UnitSpawnRequired required = ref unitSpawnRequiredPool.Get(entity);
                required.value = true;
                required.type = _unitSpawnType;
            }
        }
    }
}