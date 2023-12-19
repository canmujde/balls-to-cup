
using CMCore.Models;
using UnityEngine;

namespace CMCore.Managers
{
    public class SettingsManager
    {
        #region Properties & Fields

        public static bool VibrationsEnabled
        {
            get => Application.isPlaying &&
                   DataManager.GetPrefData(Constants.PlayerPrefsKeys.VibrationsEnabledPref, "YES") == "YES";
            private set =>
                DataManager.SetPrefData(Constants.PlayerPrefsKeys.VibrationsEnabledPref, value ? "YES" : "NO");
        }

        public static bool MusicEnabled
        {
            get => Application.isPlaying &&
                   DataManager.GetPrefData(Constants.PlayerPrefsKeys.MusicEnabledPref, "YES") == "YES";
            private set => DataManager.SetPrefData(Constants.PlayerPrefsKeys.MusicEnabledPref, value ? "YES" : "NO");
        }

        public static bool SfxEnabled
        {
            get => Application.isPlaying &&
                   DataManager.GetPrefData(Constants.PlayerPrefsKeys.SfxEnabledPref, "YES") == "YES";
            private set => DataManager.SetPrefData(Constants.PlayerPrefsKeys.SfxEnabledPref, value ? "YES" : "NO");
        }

        #endregion

        public SettingsManager()
        {
            
        }

        /// <summary>
        /// Toggles vibration and saves it's value.
        /// </summary>
        public static void ToggleVibration()
        {
            VibrationsEnabled = !VibrationsEnabled;
            DataManager.SetPrefData(Constants.PlayerPrefsKeys.VibrationsEnabledPref, VibrationsEnabled ? "YES" : "NO");
        }

        /// <summary>
        /// Toggles music and saves it's value.
        /// </summary>
        public static void ToggleMusic()
        {
            MusicEnabled = !MusicEnabled;
            DataManager.SetPrefData(Constants.PlayerPrefsKeys.MusicEnabledPref, MusicEnabled ? "YES" : "NO");
            // GameManager.AudioManager.MusicSource.volume =
            //     MusicEnabled ? GameManager.Instance.Core.TechnicalSettings.MaxMusicVolume : 0;
        }

        /// <summary>
        /// Toggles sfx and saves it's value.
        /// </summary>
        public static void ToggleSfx()
        {
            SfxEnabled = !SfxEnabled;
            DataManager.SetPrefData(Constants.PlayerPrefsKeys.SfxEnabledPref, SfxEnabled ? "YES" : "NO");
        }
    }
}