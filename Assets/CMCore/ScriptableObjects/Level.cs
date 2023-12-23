
using CMCore.Models;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CMCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level", menuName = "CMCore/Level")]
    public class Level : ScriptableObject
    {
        [field: SerializeField] public Object TubeSvg { get; private set; }
        [field: SerializeField, Range(-3f, 3f)] public float SvgTubeExtraXOffset { get; private set; }
        [field: SerializeField] public StaticTube PreMeshTube { get; private set; }
        [field: SerializeField, Range(20,100)] public int BallCount { get; private set; }
        [field: SerializeField, Range(0.15f,1)] public float BallScale { get; private set; }
         
         
    }

}