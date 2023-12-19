using System;
using System.Collections;
using CMCore.Behaviors.UI.Page;
using CMCore.Behaviors.UI.Page.Children;
using CMCore.Contracts;
using CMCore.Models;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CMCore.Managers
{
    public class UIManager
    {
        #region Properties & Fields

        public InGameUI InGameUI => _inGameUI == null ? _inGameUI = Object.FindObjectOfType<InGameUI>() : _inGameUI;
        private InGameUI _inGameUI;
        
        public WinUI WinUI => _winUI == null ? _winUI = Object.FindObjectOfType<WinUI>() : _winUI;
        private WinUI _winUI;
        
        public FailUI FailUI => _failUI == null ? _failUI = Object.FindObjectOfType<FailUI>() : _failUI;
        private FailUI _failUI;
        
        
        public PauseUI PauseUI => _pauseUI == null ? _pauseUI = Object.FindObjectOfType<PauseUI>(true) : _pauseUI;
        private PauseUI _pauseUI;
        
        public LoadingUI LoadingUI => _loadingUI == null ? _loadingUI = Object.FindObjectOfType<LoadingUI>(true) : _loadingUI;
        private LoadingUI _loadingUI;

        public Canvas Canvas => _canvas == null ? _canvas = GameObject.Find("UI").GetComponent<Canvas>() : _canvas;
        private Canvas _canvas;
        
        
        public UIBase[] StateUis
        {
            get
            {
                if (_stateUis == null || _stateUis.Length < 3)
                    return _stateUis = new UIBase[3] { _inGameUI, _winUI, _failUI };
                
                return _stateUis;
            }
        }

        private UIBase[] _stateUis;

        #endregion
   
        
        public UIManager()
        {
            GameManager.EventManager.GameStateChanged += ChangeStateUI;
            var coreUI = PoolManager.Retrieve("CoreUI", GameManager.Instance.transform);
            FailUI.Initialize(this);
            LoadingUI.Initialize(this);
            WinUI.Initialize(this);
            InGameUI.Initialize(this);
            PauseUI.Initialize(this);
        }
        
        private void ChangeStateUI(Enums.GameState state)
        {
            switch (state)
            {
                case Enums.GameState.Menu:
                    FailUI.Hide();
                    InGameUI.Hide();
                    WinUI.Hide();
                    PauseUI.Hide();
                    LoadingUI.Show();
                    break;
                case Enums.GameState.InGame:
                    FailUI.Hide();
                    InGameUI.Show();
                    WinUI.Hide();
                    PauseUI.Hide();
                    break;
                case Enums.GameState.Fail:
                    FailUI.Show();
                    InGameUI.Hide();
                    WinUI.Hide();
                    PauseUI.Hide();
                    break;
                case Enums.GameState.Win:
                    FailUI.Hide();
                    InGameUI.Hide();
                    WinUI.Show();
                    PauseUI.Hide();
                    break;
            }
        }

      


    }
}