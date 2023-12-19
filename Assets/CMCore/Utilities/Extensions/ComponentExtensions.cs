using UnityEngine;

namespace CMCore.Utilities.Extensions
{
    public static class ComponentExtensions
    {
        /// <summary>
        /// A shorter way of testing if a game object has a component
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="obj">The component to check on</param>
        /// <returns></returns>
        public static bool Has<T>(this Component obj) where T : Component
        {
            return obj.GetComponent<T>() != null;
        }

        /// <summary>
        /// A slightly shorter way to get a component from an object
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="obj">The component to get from</param>
        /// <returns></returns>
        public static T Get<T>(this Component obj) where T : Component
        {
            return obj.GetComponent<T>();
        }

        /// <summary>
        /// A slightly shorter way to get a component from an object
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="obj">The component to get from</param>
        /// <returns></returns>
        public static T[] GetAll<T>(this Component obj) where T : Component
        {
            return obj.GetComponents<T>();
        }
        
        /// <summary>
        /// Deactivates the GameObject this component is attached to
        /// </summary>
        /// <returns></returns>
        public static void Deactivate(this Component component)
        {
            component.gameObject.SetActive(false);
        }

        /// <summary>
        /// Activate the GameObject this component is attached to
        /// </summary>
        /// <returns></returns>
        public static void Activate(this Component component)
        {
            component.gameObject.SetActive(true);
        }
    }
}
