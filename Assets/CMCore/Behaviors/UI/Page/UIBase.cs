using CMCore.Contracts;
using CMCore.Managers;
using CMCore.Models;
using UnityEngine;

namespace CMCore.Behaviors.UI.Page
{
    public class UIBase : MonoBehaviour, IUI
    {
        protected UIManager Manager;
        public void Hide()
        {
            OnHide();
        }
        public void Show()
        {
            OnShow();
        }
        public virtual void Initialize(UIManager uiManager)
        {
            Manager = uiManager;
        }
        
        protected virtual void OnHide()
        {
            gameObject.SetActive(false);
        }
        
        protected virtual void OnShow()
        {
            gameObject.SetActive(true);
        }
    }
}