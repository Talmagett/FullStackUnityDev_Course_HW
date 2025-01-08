using TMPro;
using UnityEngine;

namespace Game.Views
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;

        public void SetMoneyText(string money)
        {
            moneyText.text = money;
        }
    }
}