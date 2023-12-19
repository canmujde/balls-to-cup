using Sirenix.OdinInspector;
using UnityEngine;

namespace CMCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameAssets", menuName = "CMCore/Game Assets")]
    public class GameAssets : ScriptableObject
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private AudioClip[] audioClips;

        public Sprite[] Sprites => sprites;
        public AudioClip[] AudioClips => audioClips;
    }
}