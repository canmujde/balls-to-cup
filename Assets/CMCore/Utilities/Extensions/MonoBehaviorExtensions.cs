using CMCore.Behaviors.Object;
using CMCore.Managers;
using CMCore.ScriptableObjects;
using UnityEngine;

namespace CMCore.Utilities.Extensions
{
    public static class MonoBehaviorExtensions
    {

        public static PrefabBehavior GetFromPool(this MonoBehaviour mono, string id, Transform setParent = null)
        {
            return PoolManager.Retrieve(id, setParent);
        }
        public static void RePool(this MonoBehaviour mono, PrefabBehavior prefabBehavior)
        {
            // GameManager.PoolManager.RePoolPrefab?.Invoke(prefabBehavior);
        }

        public static Core Core (this MonoBehaviour mono)  => GameManager.Instance.Core;
        public static GameplaySettings Settings(this MonoBehaviour mono)  => GameManager.Instance.Core.GameplaySettings;

        // public static Level CurrentLevel(this MonoBehaviour mono) => GameManager.LevelManager.Current;
    }
}
