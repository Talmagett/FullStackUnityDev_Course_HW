using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private Transform moneyTransform;
        [Inject] private IMoneyPresenter _moneyPresenter;
        
        private Vector3 Position => moneyTransform.position;

        private void Awake()
        {
            _moneyPresenter.SetMoneyViewPosition(Position);
            SetMoneyText(_moneyPresenter.MoneyText);
        }

        private void OnEnable()
        {
            _moneyPresenter.OnMoneyChanged += UpdateState;
            _moneyPresenter.OnMoneyEarned += SetMoneyTextWithAnimation;
        }

        private void OnDisable()
        {
            _moneyPresenter.OnMoneyChanged -= UpdateState;
            _moneyPresenter.OnMoneyEarned -= SetMoneyTextWithAnimation;
        }
        
        private void UpdateState()
        {
            SetMoneyText(_moneyPresenter.MoneyText);
        }
        
        private void SetMoneyText(string money)
        {
            moneyText.text = money;
        }

        private  void SetMoneyTextWithAnimation(int from, int to)
        {
            DOTween.To(() => from, x => SetMoneyText(x.ToString()), to, 0.5f);
        }
    }
}