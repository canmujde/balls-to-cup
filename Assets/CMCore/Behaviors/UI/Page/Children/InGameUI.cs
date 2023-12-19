using CMCore.Managers;
using CMCore.Models;
using UnityEngine;
using UnityEngine.UI;

namespace CMCore.Behaviors.UI.Page.Children
{
    public class InGameUI : UIBase
    {
        [field: SerializeField] private Button RestartButton { get; set; }
        [field: SerializeField] private PauseUI PauseUI { get; set; }
        [field: SerializeField] private Image[] FloatingImages { get; set; }
        
        #region Overriding Methods

        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            RestartButton?.onClick.AddListener(RestartButton_OnClick);
        }
        protected override void OnShow()
        {
            base.OnShow();
            UpdateCurrentLevelText(LevelManager.CurrentLevel);
            PauseUI?.Hide();
        }

        protected override void OnHide()
        {
            base.OnHide();
        }

        #endregion


        private void UpdateCurrentLevelText(int level)
        {
            // currentLevelText.text = "Level " + level;
        }


        private void RestartButton_OnClick()
        {
            HapticManager.Play(Enums.Haptic.H1);
            GameManager.EventManager.GameStateChanged?.Invoke(Enums.GameState.Menu);
            GameManager.EventManager.GameStateChanged?.Invoke(Enums.GameState.InGame);
        }

        private void PauseButton_OnClick()
        {
            PauseUI.Show();
            HapticManager.Play(Enums.Haptic.H1);
        }


        
        ///////////////////////////// /////////////////////////////

        
        

        
   }
}