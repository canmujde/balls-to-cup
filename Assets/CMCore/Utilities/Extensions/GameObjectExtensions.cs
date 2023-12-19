using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CMCore.Utilities.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// A shorter way of testing if a game object has a component
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="obj">The object to check on</param>
        /// <returns></returns>
        public static bool Has<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() != null;
        }

        /// <summary>
        /// A slightly shorter way to get a component from an object
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="obj">The object to get from</param>
        /// <returns></returns>
        public static T Get<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>();
        }

        /// <summary>
        /// A slightly shorter way to get a component from an object
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="obj">The object to get from</param>
        /// <returns></returns>
        public static T[] GetAll<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponents<T>();
        }

        /// <summary>
        /// A shortcut for creating a new game object then adding a component then adding it to a parent object
        /// </summary>
        /// <typeparam name="T">Type of component</typeparam>
        /// <returns>The new component</returns>
        public static T AddChild<T>(this GameObject parent) where T : Component
        {
            return AddChild<T>(parent, typeof(T).Name);
        }

        /// <summary>
        /// A shortcut for creating a new game object then adding a component then adding it to a parent object
        /// </summary>
        /// <typeparam name="T">Type of component</typeparam>
        /// <param name="parent"></param>
        /// <param name="name">Name of the child game object</param>
        /// <returns>The new component</returns>
        public static T AddChild<T>(this GameObject parent, string name) where T : Component
        {
            var obj = AddChild(parent, name, typeof(T));
            return obj.GetComponent<T>();
        }

        /// <summary>
        /// A shortcut for creating a new game object with a number of components and adding it as a child
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="components">A list of component types</param>
        /// <returns>The new game object</returns>
        public static GameObject AddChild(this GameObject parent, params Type[] components)
        {
            return AddChild(parent, "Game Object", components);
        }

        /// <summary>
        /// A shortcut for creating a new game object with a number of components and adding it as a child
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name">The name of the new game object</param>
        /// <param name="components">A list of component types</param>
        /// <returns>The new game object</returns>
        public static GameObject AddChild(this GameObject parent, string name, params Type[] components)
        {
            var obj = new GameObject(name, components);
            if (parent != null)
            {
                if (obj.transform is RectTransform) obj.transform.SetParent(parent.transform, true);
                else obj.transform.parent = parent.transform;
            }

            return obj;
        }
        

        /// <summary>
        /// Destroys all the children of a given GameObject
        /// </summary>
        /// <param name="parent">The parent game object</param>
        public static void DestroyAllChildrenImmediately(this GameObject parent)
        {
            parent.transform.DestroyAllChildrenImmediately();
        }
        
        /// <summary> 
        /// More elegant way of writing Destroy(gameObject).
        /// </summary>
        public static void Destroy(this GameObject go)
        {
            Object.Destroy(go);
        }

        /// <summary> 
        /// More elegant way of writing DestroyImmediate(gameObject).
        /// For use in the Editor only!
        /// </summary>
        public static void DestroyImmediate(this GameObject go)
        {
            Object.DestroyImmediate(go);
        }

        /// <summary>
        /// Changes layer of the GameObject
        /// </summary>
        /// <returns></returns>
        public static void ChangeLayer(this GameObject gameObject, string layer)
        {
            gameObject.layer = LayerMask.NameToLayer(layer);
        }

        /// <summary>
        /// Writes the object to the Debug Console.
        /// </summary>
        /// <param name="msg">Object to be logged.</param>
        /// <param name="type">Type of log.</param>
        /// <returns></returns>
        public static void Log(this object msg, LogType type = LogType.Log)
        {
            switch (type)
            {
                case LogType.Log:
                    UnityEngine.Debug.Log(msg);
                    break;
                case LogType.Warning:
                    UnityEngine.Debug.LogWarning(msg);
                    break;
                case LogType.Exception:
                case LogType.Error:
                    UnityEngine.Debug.LogError(msg);
                    break;
                case LogType.Assert:
                    UnityEngine.Debug.LogAssertion(msg);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}