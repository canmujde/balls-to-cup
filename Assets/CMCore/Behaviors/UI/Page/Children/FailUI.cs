using CMCore.Managers;
using CMCore.Models;
using UnityEngine;
using UnityEngine.UI;

namespace CMCore.Behaviors.UI.Page.Children
{
    public class FailUI : UIBase
    {
        #region Overriding Methods



        protected override void OnShow()
        {
            base.OnShow();
        }

        protected override void OnHide()
        {
            base.OnHide();
        }

        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            RetryButton.onClick.AddListener(RetryButton_OnClick);
        }
        
        #endregion
        
        private void RetryButton_OnClick()
        {
            GameManager.EventManager.GameStateChanged?.Invoke(Enums.GameState.Menu);
            GameManager.EventManager.GameStateChanged?.Invoke(Enums.GameState.InGame);
            HapticManager.Play(Enums.Haptic.H1);
        }
        
        
        /////////////////////////////
        
        
        [field: SerializeField] public Button RetryButton { get; private set; }
    }
}