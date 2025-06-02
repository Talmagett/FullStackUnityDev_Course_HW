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
        private UnitSpawnType _unitSpawnType;

        [SerializeField]
        private TeamType _teamType;

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
            UnitSpawnUseCase.SpawnUnit(_unitSpawnType, _teamType);
        }
    }
}