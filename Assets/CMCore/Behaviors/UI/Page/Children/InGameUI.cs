using CMCore.Managers;
using CMCore.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CMCore.Behaviors.UI.Page.Children
{
    public class InGameUI : UIBase
    {
        [field: SerializeField] private Button RestartButton { get; set; }
        [field: SerializeField] private TextMeshProUGUI CurrentLevelText { get; set; }

        [field: SerializeField] private GameObject Tutorial { get; set; }
        

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
            
        }

        protected override void OnHide()
        {
            base.OnHide();
        }

        #endregion


        private void UpdateCurrentLevelText(int level) =>CurrentLevelText.text = "Level " + level;
        private void RestartButton_OnClick()
        {
            HapticManager.Play(Enums.Haptic.H1);
            GameManager.EventManager.GameStateChanged?.Invoke(Enums.GameState.Menu);
            GameManager.EventManager.GameStateChanged?.Invoke(Enums.GameState.InGame);
        }

        public void ToggleTutorial(bool enable)
        {
    
            Tutorial.SetActive(enable);
        }
        
        public void MarkTutorial(bool mark)
        {
            ToggleTutorial(false);
            GameManager.LevelManager.TutorialPlayed = mark;
        }
        
    }
}