using System;
using UnityEngine;

namespace CMCore.Models
{
    [Serializable]
    public class StaticTube
    {
        public Mesh m_Mesh;
        public Vector3 m_ContainerLocalPosition;
        public Vector3 m_BaseLocalPosition;
        public Vector3 m_BallReleaserLocalPosition;
        public Vector3 m_BallReleaserLocalEulerAngles;
        
    }
}
