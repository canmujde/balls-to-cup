using System;
using System.Collections.Generic;
using CMCore.Behaviors;
using CMCore.Models;
using CMCore.ScriptableObjects;
using CMCore.Utilities.Extensions;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Constants = CMCore.Models.Constants;

namespace CMCore.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        #region Properties & Fields

        [field: SerializeField] public Core Core { get; private set; }

        private Enums.GameState CurrentState { get; set; }
        private List<Enums.GameState> PreviousStates { get; set; }
        public static UIManager UIManager { get; private set; }
        public static LevelManager LevelManager { get; private set; }
        public static PoolManager PoolManager { get; private set; }
        public static HapticManager HapticManager { get; private set; }
        public static AudioManager AudioManager { get; private set; }
        public static SettingsManager SettingsManager { get; private set; }
        public static DataManager DataManager { get; private set; }
        public static CameraManager CameraManager { get; private set; }
        public static InputManager InputManager { get; private set; }
        public static EventManager EventManager { get; private set; }

        [ShowInInspector] public string State => CurrentState.ToString();

        [ShowInInspector]
        public static Level CurrentLevel =>
            Application.isPlaying ? LevelManager.Current ? LevelManager.Current : null : null;
        [ShowInInspector] public static string InputAllowed => Application.isPlaying && InputManager.InputAllowed ? "Yes" : "No" ;
        [ShowInInspector] public static string Seed => Application.isPlaying ? LevelManager.Seed.ToString() : null;

        #endregion

        #region Unity

        protected override void Awake()
        {
            base.Awake();

            SettingsManager = new SettingsManager();
            DataManager = new DataManager();
            PoolManager = new PoolManager();
            EventManager = new EventManager();
            InputManager = new InputManager();
            UIManager = new UIManager();
            LevelManager = new LevelManager();
            HapticManager = new HapticManager();
            AudioManager = new AudioManager();
            CameraManager = new CameraManager();
        }

        private void Start()
        {
            Application.targetFrameRate = Core.TechnicalSettings.TargetFrameRate;
            DOTween.SetTweensCapacity(Core.TechnicalSettings.TweenSize, Core.TechnicalSettings.SequenceSize);
            PreviousStates = new List<Enums.GameState>();
            EventManager.GameStateChanged += OnStateChanged;

            EventManager.GameStateChanged?.Invoke(Enums.GameState.Menu);
            EventManager.GameStateChanged?.Invoke(Enums.GameState.InGame);
        }

        private void Update()
        {
#if UNITY_EDITOR
            Manipulation();
#endif
        }

        #endregion


        private void Manipulation()
        {
            #region State Manipulation

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (CurrentState == Enums.GameState.InGame)
                    EventManager.GameStateChanged?.Invoke(Enums.GameState.Win);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (CurrentState == Enums.GameState.InGame)
                    EventManager.GameStateChanged?.Invoke(Enums.GameState.Fail);
            }

            #endregion

            #region Time Manipulation

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Time.timeScale = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Time.timeScale = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Time.timeScale = 3;
            }

            #endregion
        }

        private void OnStateChanged(Enums.GameState state)
        {
            PreviousStates.Add(state);
            CurrentState = state;

            if (Core.TechnicalSettings.LogEnabled)
                Debug.Log(Constants.Messages.StateChangedMessage + state);
        }
    }
}