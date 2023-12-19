using System.Collections.Generic;
using CMCore.Behaviors.Object;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace CMCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GamePrefabs", menuName = "CMCore/Game Prefabs")]
    public class GamePrefabs : ScriptableObject
    {
        [field: SerializeField] public List<PrefabBehavior> InstantiatablePrefabs { get; private set; }
    }
}