using Sirenix.OdinInspector;
using UnityEngine;

namespace CMCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameAssets", menuName = "CMCore/Game Assets")]
    public class GameAssets : ScriptableObject
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private AudioClip[] audioClips;
        [SerializeField] private Material[] ballMaterials;

        public Sprite[] Sprites => sprites;
        public AudioClip[] AudioClips => audioClips;
        public Material[] BallMaterials => ballMaterials;
    }
}