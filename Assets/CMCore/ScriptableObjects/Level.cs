
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CMCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level", menuName = "CMCore/Level")]
    public class Level : ScriptableObject
    {
        [field: SerializeField, Required] public Object TubeSvg { get; private set; }
         
         
    }

}