using System;
using CMCore.Behaviors.Object;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CMCore.Models
{
    [Serializable]
    public class InstantiatablePrefab
    {
        [field: SerializeField, AssetSelector(FlattenTreeView = true), HideReferenceObjectPicker]
        public PrefabBehavior Prefab { get; private set; }

        [field: SerializeField] public int PoolSize { get; private set; }
    }
}