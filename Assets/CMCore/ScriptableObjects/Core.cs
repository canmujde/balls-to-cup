
using Sirenix.OdinInspector;
using UnityEngine;

namespace CMCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Core", menuName = "CMCore/Core")]
    public class Core : ScriptableObject
    {
        [field: SerializeField] public bool ShowSettings { get; private set; }
        [field: SerializeField, HideIf("@!this.ShowSettings")] public GameplaySettings GameplaySettings { get; private set; }
        [field: SerializeField, HideIf("@!this.ShowSettings")] public GameAssets Assets { get; private set; }
        [field: SerializeField, HideIf("@!this.ShowSettings")] public GamePrefabs Prefabs { get; private set; }
        [field: SerializeField, HideIf("@!this.ShowSettings")] public TechnicalSettings TechnicalSettings { get; private set; }

      
        [field: SerializeField,Title("Levels")] public Level[] Levels { get; private set; }
    }
}
