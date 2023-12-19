using UnityEngine;

namespace CMCore.Models
{
    [System.Serializable]
    public class Transfigure
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        public Transfigure(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
    }
}