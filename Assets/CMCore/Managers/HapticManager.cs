using System.Collections;
using CMCore.Contracts;
using CMCore.Models;
using Lofelt.NiceVibrations;
using UnityEngine;

namespace CMCore.Managers
{
    public class HapticManager
    {

        #region Properties & Fields
        private Coroutine _continuousRoutine;


        

        #endregion

        public HapticManager()
        {
            GameManager.EventManager.GameStateChanged += OnStateChanged;
        }

        private static void OnStateChanged(Enums.GameState state)
        {
            switch (state)
            {
                case Enums.GameState.Menu:
                    break;
                case Enums.GameState.InGame:
                    break;
                case Enums.GameState.Fail:
                    OnGameEndState(Enums.GameResult.Fail);
                    break;
                case Enums.GameState.Win:
                    OnGameEndState(Enums.GameResult.Win);
                    break;
            }
        }


        private static void OnGameEndState(Enums.GameResult result)
        {
            switch (result)
            {
                case Enums.GameResult.Win:
                    Play(Enums.Haptic.Success);
                    break;
                case Enums.GameResult.Fail:
                    Play(Enums.Haptic.Failure);
                    break;
            }
        }

        /// <summary>
        /// Plays haptic by given haptic type.
        /// </summary>
        /// <param name="hapticType"></param>
        public static void Play(Enums.Haptic hapticType)
        {
            if (!SettingsManager.VibrationsEnabled) return;

            switch (hapticType)
            {
                case Enums.Haptic.H1:
#if UNITY_IOS
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
#elif UNITY_ANDROID
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
#endif
                    break;
                case Enums.Haptic.H2:
#if UNITY_IOS
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
#elif UNITY_ANDROID
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.RigidImpact);
#endif
                    break;
                case Enums.Haptic.H3:
#if UNITY_IOS
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
#elif UNITY_ANDROID
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
#endif
                    break;
                case Enums.Haptic.H4:
#if UNITY_IOS
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.RigidImpact);
#elif UNITY_ANDROID
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
#endif
                    break;
                case Enums.Haptic.H5:
#if UNITY_IOS
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
#elif UNITY_ANDROID
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
#endif
                    break;
                case Enums.Haptic.H6:
#if UNITY_IOS
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
#elif UNITY_ANDROID
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
#endif
                    break;
                case Enums.Haptic.Success:
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.Success);
                    break;
                case Enums.Haptic.Failure:
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.Failure);
                    break;
                case Enums.Haptic.Warning:
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
                    break;
            }
        }

        public static void PlayClip(HapticClip clip)
        {
            HapticController.Play(clip);
        }

        /// <summary>
        /// Plays vibration by given count continuously.
        /// </summary>
        public void PlayContinuously(int count)
        {
            if (_continuousRoutine != null) GameManager.Instance.StopCoroutine(_continuousRoutine);

            _continuousRoutine = GameManager.Instance.StartCoroutine(VibrateContinuouslyByCount(count));
        }


        private static IEnumerator VibrateContinuouslyByCount(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new WaitForSeconds(0.03f);

                Play(Enums.Haptic.H1);
            }
        }
        

     
    }
}