
using CMCore.Contracts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CMCore.Behaviors.Object
{
    [AddComponentMenu("Prefab")]
    public class PrefabBehavior : MonoBehaviour, IResettable
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public int PoolSize { get; private set; }
        
        public virtual void ResetBehavior()
        {
            
        }

        [Button]
        private void SetID()
        {
            if (gameObject.name != Id && !Application.isPlaying)
                Id = gameObject.name;
        }
       
    }
}