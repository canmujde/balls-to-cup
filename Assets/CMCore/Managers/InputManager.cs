using System;
using System.Linq;
using CMCore.Contracts;
using CMCore.Models;
using CMCore.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CMCore.Managers
{
    public class InputManager
    {
        #region Properties & Fields
        public bool InputAllowed { get; private set; }
        public static bool MouseDown => GameManager.Instance.State == "InGame" && Input.GetMouseButtonDown(0);
        public static bool MousePressing => GameManager.Instance.State == "InGame" && Input.GetMouseButton(0);

        #endregion

        public InputManager()
        {
            InputAllowed = false;
            Input.multiTouchEnabled = GameManager.Instance.Core.TechnicalSettings.MultiTouchEnabled;
        }
        
        
        public void ToggleInput(bool enable)
        {
            InputAllowed = enable;
        }
        
    }
}