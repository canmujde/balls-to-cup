using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CMCore.Tweens
{
    public class ScaleTween : MonoBehaviour
    {
        public float duration;
        public float delay;
        public bool onEnable;
        public bool punch;
        public bool loop;
        public LoopType loopType;
        public Ease ease;
        [Header("Parameters")] public Vector3 from;
        public Vector3 to = Vector3.one;


        private Transform _thisTransform;

        [Header("Punch Parameters")] public int vibrato;
        public float elasticity;
        [Header("On Complete")] public UnityEvent OnComplete;

        private void Awake()
        {
            if (!_thisTransform)
                _thisTransform = transform;
        }

        private void OnEnable()
        {
            if (onEnable)
                DoScale();
        }

        public void DoScale()
        {
            if (!_thisTransform) return;
            var id = gameObject.name + "Scale";

            DOTween.Kill(id);
            _thisTransform.localScale = from;
            if (!punch)
            {
                _thisTransform.DOScale(to, duration).SetId(id).SetLoops(loop ? -1 : 0, loopType).SetEase(ease)
                    .SetDelay(delay)
                    .OnComplete(() => OnComplete?.Invoke());
            }
            else
            {
                _thisTransform.DOPunchScale(to, duration, vibrato, elasticity).SetId(id)
                    .SetLoops(loop ? -1 : 0, loopType).SetEase(ease).SetDelay(delay)
                    .OnComplete(() => { OnComplete?.Invoke(); });
            }
        }

        public void DoScale(float durationOfScale, float delayBeforeStart, Ease easeType, bool isLoop, bool isPunch,
            Vector3 startFrom, Vector3 scaleTo, Action onComplete, int vibrate = 1, float elastic = 1)
        {
            var id = gameObject.name + "Scale";

            DOTween.Kill(id);
            _thisTransform.localScale = startFrom;
            if (!isPunch)
            {
                _thisTransform.DOScale(scaleTo, durationOfScale).SetId(id).SetLoops(isLoop ? -1 : 0, loopType)
                    .SetEase(easeType).SetDelay(delayBeforeStart)
                    .OnComplete(() => onComplete?.Invoke());
            }
            else
            {
                _thisTransform.DOPunchScale(scaleTo, durationOfScale, vibrate, elastic).SetId(id)
                    .SetLoops(isLoop ? -1 : 0, loopType).SetEase(easeType).SetDelay(delayBeforeStart)
                    .OnComplete(() => { onComplete?.Invoke(); });
            }
        }
    }
}