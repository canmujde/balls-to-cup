using TMPro;
using UnityEngine;

namespace CMCore.Behaviors.Object
{
    public class ExistingBallIndicator : PrefabBehavior
    {
        private Transform _tubeContainer;
        [SerializeField] private TextMeshPro text;

        private const float c_movementSpeed = 5f;

        public override void ResetBehavior()
        {
            base.ResetBehavior();
        }

        public void SetTubeContainer(Transform t) => _tubeContainer = t;

        public void UpdateText(int count)
        {
            text.text = count.ToString();
        }

        void Update()
        {
            if (!_tubeContainer) return;

            var to = new Vector3(
                _tubeContainer.transform.position.x > 0
                    ? _tubeContainer.transform.position.x - 0.25f
                    : _tubeContainer.transform.position.x + 0.25f, _tubeContainer.transform.position.y, 4);
            transform.position = Vector3.Slerp(transform.position, to, Time.deltaTime * c_movementSpeed);
        }
    }
}