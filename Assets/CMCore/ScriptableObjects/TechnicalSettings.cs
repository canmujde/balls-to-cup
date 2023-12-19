using Newtonsoft.Json;
using UnityEngine;

namespace CMCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TechnicalSettings", menuName = "CMCore/Technical Settings")]
    public class TechnicalSettings : ScriptableObject
    {
        
        [field: SerializeField] public bool LogEnabled { get; private set; }
        [field: SerializeField] public bool MultiTouchEnabled { get; private set; }
        [field: SerializeField] public int TargetFrameRate { get; private set; }

        [field: SerializeField] public int PoolExtendSize { get; private set; }
        [field: SerializeField] public int TweenSize { get; private set; }
        [field: SerializeField] public int SequenceSize { get; private set; }
        [field: SerializeField] public int RepeatLastNLevelsAfterFinishedAllLevels { get; private set; }
        [field: SerializeField] public float MaxMusicVolume { get; private set; }
    }
}
