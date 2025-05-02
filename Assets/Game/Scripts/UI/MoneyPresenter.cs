using TMPro;
using UnityEngine;

namespace SampleGame
{
    public sealed class MoneyPresenter : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _moneyView;
        
        private PlayerData _playerData;

        private void Start()
        {
            _playerData = EcsAdmin.Systems.GetWorld().GetSingleton<PlayerData>();
        }
        
        private void LateUpdate()
        {
            _moneyView.SetText($"Money: {_playerData.money}");
        }
    }
}