using CMCore.Managers;
using CMCore.Models;
using CMCore.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace CMCore.Behaviors.UI.Page.Children
{
    public class PauseUI : UIBase
    {
        private void Update()
        {
            pauseDuration += Time.deltaTime;
        }

        protected override void OnHide()
        {
            base.OnHide();
        }

        protected override void OnShow()
        {
            base.OnShow();

            SetVibrationButton();
            SetMusicButton();
            SetSfxButton();
        }

        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            restartButton.onClick.AddListener(RestartButton_OnClick);
            resumeButton.onClick.AddListener(ResumeButton_OnClick);
            vibrationButton.onClick.AddListener(ToggleVibration);
            sfxButton.onClick.AddListener(ToggleSfx);
            musicButton.onClick.AddListener(ToggleMusic);

            SetVibrationButton();
            SetMusicButton();
            SetSfxButton();
        }

        private void RestartButton_OnClick()
        {
            GameManager.EventManager.GameStateChanged?.Invoke(Enums.GameState.Menu);
            GameManager.EventManager.GameStateChanged?.Invoke(Enums.GameState.InGame);
            HapticManager.Play(Enums.Haptic.H1);
            Hide();
        }
        private void SetSfxButton()
        {
            var isEnabled = SettingsManager.SfxEnabled;

            sfxButton.image.color = isEnabled ? Color.white : Color.black;
            soundOffImage.SetActive(!isEnabled);
            soundOnImage.SetActive(isEnabled);

            // sfxToggleButtonImage.SetNativeSize();
        }

        private void SetMusicButton()
        {
            var isEnabled = SettingsManager.MusicEnabled;


            musicButton.image.color = isEnabled ? Color.white : Color.black;
            musicOffImage.SetActive(!isEnabled);
            musicOnImage.SetActive(isEnabled);
            // musicToggleButtonImage.SetNativeSize();
        }

        private void SetVibrationButton()
        {
            var isEnabled = SettingsManager.VibrationsEnabled;

            vibrationButton.image.color = isEnabled ? Color.white : Color.black;
            hapticOffImage.SetActive(!isEnabled);
            hapticOnImage.SetActive(isEnabled);
        }

        private void ResumeButton_OnClick()
        {
            Hide();
            HapticManager.Play(Enums.Haptic.H1);
        }

        private void ToggleVibration()
        {
            SettingsManager.ToggleVibration();
            HapticManager.Play(Enums.Haptic.H1);
            SetVibrationButton();
        }

        private void ToggleMusic()
        {
            SettingsManager.ToggleMusic();
            HapticManager.Play(Enums.Haptic.H1);
            SetMusicButton();
        }

        private void ToggleSfx()
        {
            SettingsManager.ToggleSfx();
            HapticManager.Play(Enums.Haptic.H1);
            SetSfxButton();
        }
        
        
        
        

        
        
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button vibrationButton;
        [SerializeField] private Button sfxButton;
        [SerializeField] private Button musicButton;


        [SerializeField] private GameObject hapticOnImage;
        [SerializeField] private GameObject hapticOffImage;

        [SerializeField] private GameObject soundOnImage;
        [SerializeField] private GameObject soundOffImage;

        [SerializeField] private GameObject musicOnImage;
        [SerializeField] private GameObject musicOffImage;


        public float pauseDuration;
        
        
    }
}