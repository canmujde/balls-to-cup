using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CMCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "CMCore/Gameplay Settings")]
    public class GameplaySettings : ScriptableObject
    {
        
        [field: SerializeField] public string BaseURL { get; private set; }
        [field: SerializeField] public string GameName { get; private set; }
        [field: SerializeField] public string FileExtensionOnRemote { get; private set; }
        [field: SerializeField] public int MaximumAttemptToRequestData { get; private set; }
        [field: SerializeField] public string FullURL { get; private set; }
        
        [BoxGroup]
        [DisplayAsString(false), ShowInInspector, HideLabel] public string RemoteURL { get; private set; }

        private void OnValidate()
        {
            RemoteURL = BaseURL + GameName + FileExtensionOnRemote;
        }
    }
    
}