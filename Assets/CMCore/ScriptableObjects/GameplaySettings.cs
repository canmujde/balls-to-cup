using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CMCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "CMCore/Gameplay Settings")]
    public class GameplaySettings : ScriptableObject
    {
        [field: SerializeField] public float RotationSensitivity { get; private set; }
        
        [field: SerializeField, Range(0,1)] public float BallBounciness { get; private set; } 
        [field: SerializeField, Range(0,100)] public float BallFriction { get; private set; }
        [field: SerializeField, Range(0,100)] public float BallAirFriction { get; private set; }
        
        [field: SerializeField] public PhysicMaterial BallPhysicMaterial { get; private set; }

        private void OnValidate()
        {
            if (BallPhysicMaterial==null) return;
            BallPhysicMaterial.bounciness = BallBounciness;
            BallPhysicMaterial.dynamicFriction = BallPhysicMaterial.staticFriction = BallFriction;
        }
    }
    
}