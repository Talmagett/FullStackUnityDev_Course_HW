using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.Views
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private Transform moneyTransform;
        
        public Vector3 Position => moneyTransform.position;

        [Button]
        public void SetMoneyText(string money)
        {
            moneyText.text = money;
        }
    }
}