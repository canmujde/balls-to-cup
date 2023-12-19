using System;
using UnityEngine;

namespace CMCore.Utilities
{
    [Serializable]
    public class Raycaster
    {
        public Transform StartTransform;
        public Vector3 Direction;
        public float RayLength;
        public int layerMask = 0;

        public event Action<Collider, Raycaster> OnRayEnter;
        public event Action<Collider, Raycaster> OnRayStay;
        public event Action<Collider, Raycaster> OnRayExit;

        Collider previous;
        RaycastHit hit = new RaycastHit();

        public bool CastRay()
        {
            Physics.Raycast(StartTransform.position, Direction, out hit, RayLength);
            Debug.DrawRay(StartTransform.position, Direction, Color.green, 2);
            ProcessCollision(hit.collider, this);
            return hit.collider != null ? true : false;
        }

        private void ProcessCollision(Collider current, Raycaster raycaster)
        {
            // No collision this frame.
            if (current == null)
            {
                // But there was an object hit last frame.
                if (previous != null)
                {
                    DoEvent(OnRayExit, previous, raycaster);
                }
            }

            // The object is the same as last frame.
            else if (previous == current)
            {
                DoEvent(OnRayStay, current, raycaster);
            }

            // The object is different than last frame.
            else if (previous != null)
            {
                DoEvent(OnRayExit, previous,raycaster);
                DoEvent(OnRayEnter, current,raycaster);
            }

            // There was no object hit last frame.
            else
            {
                DoEvent(OnRayEnter, current,raycaster);
            }

            // Remember this object for comparing with next frame.
            previous = current;
        }


        private void DoEvent(Action<Collider, Raycaster> action, Collider collider, Raycaster raycaster)
        {
            if (action != null)
            {
                action(collider, raycaster);
            }
        }

        public static int GetLayerMask(string layerName, int existingMask = 0)
        {
            int layer = LayerMask.NameToLayer(layerName);
            return existingMask | (1 << layer);
        }
    }
}