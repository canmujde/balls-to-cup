using System.Collections.Generic;
using System.Linq;
using CMCore.Behaviors.Object;
using CMCore.Contracts;
using CMCore.Utilities.Extensions;
using UnityEngine;


namespace CMCore.Managers
{
    public class PoolManager
    {
        #region Properties & Fields

        private static List<PrefabBehavior> UsedPrefabs { get; set; }
        private static List<PrefabBehavior> PooledPrefabs { get; set; }

        private static Transform PoolParent =>
            _poolParent == null ? _poolParent = GameObject.Find("CorePool").transform : _poolParent;

        private static Transform _poolParent;

        #endregion

        public PoolManager()
        {
            UsedPrefabs = new List<PrefabBehavior>();
            PooledPrefabs = new List<PrefabBehavior>();
            foreach (var prefab in GameManager.Instance.Core.Prefabs.InstantiatablePrefabs)
                CreateStack(prefab, prefab.PoolSize);
        }

        private static void CreateStack(PrefabBehavior prefab, int size)
        {
            for (int i = 0; i < size; i++)
            {
                var instantiatedPrefab = Object.Instantiate(prefab.gameObject, PoolParent);
                var prefabBehavior = instantiatedPrefab.GetComponent<PrefabBehavior>();
                instantiatedPrefab.SetActive(false);
                instantiatedPrefab.name = prefab.gameObject.name;
                PooledPrefabs.Add(prefabBehavior);
            }
        }

        public static PrefabBehavior Retrieve(string prefabId, Transform parent = null)
        {
            var isExistInPrefabLibrary =
                GameManager.Instance.Core.Prefabs.InstantiatablePrefabs.Any(instantiatablePrefab =>
                    instantiatablePrefab.Id == prefabId);
            if (!isExistInPrefabLibrary)
            {
                if (GameManager.Instance.Core().TechnicalSettings.LogEnabled)
                    Debug.Log(
                        $"No object in pool library named : {prefabId}. Are you declared this prefab to GamePrefabs?");
                return null;
            }

            var prefab = PooledPrefabs.Find(prefab => prefab.Id == prefabId);

            if (prefab)
            {
                UsedPrefabs.Add(prefab);
                prefab.gameObject.SetActive(true);
                prefab.transform.SetParent(parent == null ? GameManager.LevelManager.LevelRoot : parent);
                if (PooledPrefabs.Contains(prefab))
                    PooledPrefabs.Remove(prefab);
                return prefab;
            }


            //increase pool size & recursive call.
            if (GameManager.Instance.Core().TechnicalSettings.LogEnabled)
                Debug.Log(
                    $"No object in pool with id : {prefabId}. Extending pool by {GameManager.Instance.Core.TechnicalSettings.PoolExtendSize}.");

            prefab = GameManager.Instance.Core.Prefabs.InstantiatablePrefabs.Find(instantiatablePrefab =>
                instantiatablePrefab.Id == prefabId);

            CreateStack(prefab, GameManager.Instance.Core.TechnicalSettings.PoolExtendSize);
            return Retrieve(prefabId, parent);
        }

        public static void Return(PrefabBehavior prefab)
        {
            if (UsedPrefabs.Contains(prefab))
                UsedPrefabs.Remove(prefab);
            PooledPrefabs.Add(prefab);
            prefab.transform.SetParent(PoolParent);
            prefab.gameObject.name = prefab.Id;
            prefab.gameObject.SetActive(false);
        }

        public static void ReturnAll()
        {
            foreach (var prefab in UsedPrefabs.ToList())
            {
                if (prefab is INonReturnable) continue;
                Return(prefab);
            }
        }
    }
}