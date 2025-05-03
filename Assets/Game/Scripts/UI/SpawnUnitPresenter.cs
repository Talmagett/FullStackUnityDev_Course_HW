using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public sealed class SpawnUnitPresenter : MonoBehaviour
    {
        [SerializeField]
        private Button _spawnUnitButton;

        [SerializeField]
        private TeamType _teamType;
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
            
        }
    }
}