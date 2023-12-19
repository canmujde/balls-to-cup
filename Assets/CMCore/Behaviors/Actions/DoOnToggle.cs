using UnityEngine;
using UnityEngine.Events;

namespace CMCore.Behaviors.Actions
{
    public class DoOnToggle : MonoBehaviour
    {
        [SerializeField] private UnityEvent doOnEnable;
        [SerializeField] private UnityEvent doOnDisable;
    

        private void OnEnable()
        {
            doOnEnable?.Invoke();
        }

        private void OnDisable()
        {
            doOnDisable?.Invoke();
        }
        
    }
}