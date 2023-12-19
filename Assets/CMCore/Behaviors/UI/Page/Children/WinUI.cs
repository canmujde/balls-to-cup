using CMCore.Managers;
using CMCore.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace CMCore.Behaviors.UI.Page.Children
{
    public class WinUI : UIBase
    {

        public TextMeshProUGUI levelText;

        public Image motivationalText;

        public Sprite[] motivationalSprites;
        

        [SerializeField] private Button nextButton;

        private void NextButton_OnClick()
        {
            GameManager.EventManager.GameStateChanged?.Invoke(Enums.GameState.Menu);
            GameManager.EventManager.GameStateChanged?.Invoke(Enums.GameState.InGame);
            HapticManager.Play(Enums.Haptic.H1);
        }

        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            nextButton.onClick.AddListener(NextButton_OnClick);
        }

        protected override void OnShow()
        {
            base.OnShow();
            levelText.text = "LEVEL " + (LevelManager.CurrentLevel - 1);
            motivationalText.sprite = motivationalSprites[Random.Range(0, motivationalSprites.Length)]; 

        }
        

        protected override void OnHide()
        {
            base.OnHide();
        }
    }
}