using System.Collections.Generic;
using System.Threading.Tasks;
using CMCore.Behaviors.Object;
using CMCore.Contracts;
using CMCore.Models;
using CMCore.ScriptableObjects;
using CMCore.Utilities.Extensions;
using UnityEngine;


namespace CMCore.Managers
{
    public class LevelManager
    {
        #region Properties

        private Transform _levelRoot;


        public Transform LevelRoot => Application.isPlaying
            ? _levelRoot == null ? _levelRoot = GameObject.Find("CoreRuntime").transform : _levelRoot
            : null;

        public int Seed { get; private set; }

        public Level Current { get; private set; }

        public static int CurrentLevelId
        {
            get => Application.isPlaying
                ? DataManager.GetPrefData(Constants.PlayerPrefsKeys.CurrentLevelIDPref,
                    Constants.Defaults.CurrentLevelIDPrefDefault)
                : Constants.Defaults.CurrentLevelIDPrefDefault;
            private set => DataManager.SetPrefData(Constants.PlayerPrefsKeys.CurrentLevelIDPref, value);
        }

        public static int CurrentLevel
        {
            get => Application.isPlaying
                ? DataManager.GetPrefData(Constants.PlayerPrefsKeys.CurrentLevelPref,
                    Constants.Defaults.CurrentLevelPrefDefault)
                : -1;
            private set => DataManager.SetPrefData(Constants.PlayerPrefsKeys.CurrentLevelPref, value);
        }

        public bool TutorialPlayed
        {
            get => Application.isPlaying && DataManager.GetPrefData("TutorialPlayed", "NO") == "YES";
            set => DataManager.SetPrefData("TutorialPlayed", value ? "YES" : "NO");
        }


        public LevelBehavior LevelBehavior;

        #endregion

        public LevelManager()
        {
            GameManager.EventManager.GameStateChanged += OnStateChanged;
        }

        private void OnStateChanged(Enums.GameState state)
        {
            switch (state)
            {
                case Enums.GameState.Menu:
                    Current = Create();
                    LevelBehavior = PoolManager.Retrieve("LevelBehavior").GetComponent<LevelBehavior>();
                    LevelBehavior.ResetBehavior();

                    break;
                case Enums.GameState.InGame:
                    break;
                case Enums.GameState.Fail:
                    break;
                case Enums.GameState.Win:
                    Success();
                    break;
            }
        }

        private Level Create()
        {
            if (Current) PoolManager.ReturnAll();

            Seed = RandomExtensions.RandomSeed();
            var level = GetLevelByIndex(CurrentLevelId);
            return level;
        }

        private static Level GetLevelByIndex(int orderIndex)
        {
            if (orderIndex >= GameManager.Instance.Core.Levels.Length) goto restart;
            var level = GameManager.Instance.Core.Levels[orderIndex];
            if (level) return level;

            restart:
            CurrentLevelId = GameManager.Instance.Core.TechnicalSettings.RepeatLastNLevelsAfterFinishedAllLevels > 0 &&
                             GameManager.Instance.Core.TechnicalSettings.RepeatLastNLevelsAfterFinishedAllLevels <
                             GameManager.Instance.Core.Levels.Length
                ? GameManager.Instance.Core.Levels.Length - GameManager.Instance.Core.TechnicalSettings
                    .RepeatLastNLevelsAfterFinishedAllLevels
                : 0;
            level = GameManager.Instance.Core.Levels[CurrentLevelId];

            return level;
        }

        private static void Success()
        {
            CurrentLevelId += 1;
            CurrentLevel += 1;
        }
    }
}