using Atomic.UI;
using UnityEngine;

namespace Game.Scripts.UI.Views
{
    public class PopupSettingsPresenter : Presenter
    {
        [SerializeField] private PopupSettingsView popupSettingsView;
        protected override void OnInit()
        {
            popupSettingsView.OnCloseButtonClicked += Hide;
        }
    }
}