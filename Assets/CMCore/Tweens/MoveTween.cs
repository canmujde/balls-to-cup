using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CMCore.Tweens
{
    public class MoveTween : MonoBehaviour
    {
        public float duration;
        public float delay;
        public Ease ease;
        public LoopType loopType;
        public bool loop;
        public bool onEnable;
        [Header("Parameters")] public Vector3 from;
        private Vector3 _to;
        [Header("On Complete")]
        public UnityEvent OnComplete;

        private Transform _thisTransform;

        private void Awake()
        {
            _thisTransform = transform;
            _to = _thisTransform.localPosition;

        }

        private void OnEnable()
        {
            
            if (onEnable)
                DoMove();
        }

        public void DoMove()
        {
            if (!_thisTransform) return;
            var id = gameObject.name + "Move";

            DOTween.Kill(id);
            _thisTransform.localPosition = from;

            _thisTransform.DOLocalMove(_to, duration).SetId(id).SetLoops(loop ? -1 : 0, loopType).SetEase(ease).SetDelay(delay)
                .OnComplete(() => OnComplete?.Invoke());
        }

        public void DoMove(float durationOfMove,float delayBeforeStart, Ease easeType, bool isLoop, Vector3 startFrom, Vector3 goTo, Action onComplete)
        {
            var id = gameObject.name + "Move";

            DOTween.Kill(id);
            _thisTransform.localPosition = startFrom;

            _thisTransform.DOLocalMove(goTo, durationOfMove).SetId(id).SetLoops(isLoop ? -1 : 0, loopType).SetEase(easeType).SetDelay(delayBeforeStart)
                .OnComplete(() => onComplete?.Invoke());
        }

    }
}