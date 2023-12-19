using System.Collections.Generic;
using System.Linq;
using CMCore.Behaviors.Object;
using CMCore.Managers;
using UnityEngine;

namespace CMCore.Utilities.Extensions
{
    public static class TransformExtensions
    {
        public static Vector3 Center(this List<Transform> transforms)
        {
            var bound = new Bounds(transforms[0].position, Vector3.zero);
            for (int i = 1; i < transforms.Count; i++)
            {
                bound.Encapsulate(transforms[i].position);
            }

            return bound.center;
        }
    
        public static void SortByDistance(this List<Transform> transforms, Vector3 measureFrom)
        {
            transforms.OrderBy(x => Vector3.Distance(x.position, measureFrom));
        }
        
        /// <summary>
        /// Re-Pool all the children
        /// </summary>
        /// <param name="parent">The parent game object</param>
        public static void RePoolAllChildrenImmediately(this Transform parent)
        {
            while (parent.childCount != 0)
            {
                var prefab = parent.GetChild(0).Get<PrefabBehavior>();
                if (!prefab) continue;
                // GameManager.EventManager.RePoolPrefab?.Invoke(prefab);
            }
        }
        
        /// <summary>
        /// Destroys all the children of a given transform
        /// </summary>
        /// <param name="parent"></param>
        public static void DestroyAllChildrenImmediately(this Transform parent)
        {
            while (parent.childCount != 0)
                Object.DestroyImmediate(parent.GetChild(0).gameObject);
        }

    }
}
