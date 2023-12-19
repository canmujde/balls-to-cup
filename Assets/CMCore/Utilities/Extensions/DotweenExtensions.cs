using DG.Tweening;
using UnityEngine;

namespace CMCore.Utilities.Extensions
{
    // ReSharper disable once IdentifierTypo
    public static class DotweenExtensions
    {
        // ReSharper disable once InconsistentNaming
        public static Tween DOBlendShapeWeight(this SkinnedMeshRenderer skinnedMeshRenderer, int index, float end, float duration)
        {
            return DOTween.To(() => skinnedMeshRenderer.GetBlendShapeWeight(index),
                x => skinnedMeshRenderer.SetBlendShapeWeight(index, x),
                end, duration);
        }

        // ReSharper disable once InconsistentNaming
        public static Tween DOLineRendererWidth(this LineRenderer line, float startValue, float endValue, float duration)
        {
            line.widthMultiplier = startValue;
            return DOTween.To(() => line.widthMultiplier, val => line.widthMultiplier = val, endValue, duration);
        }

        // ReSharper disable once InconsistentNaming
        public static Tween DOLineRendererPosition(this LineRenderer line, int index, Vector3 startValue, Vector3 endValue, float duration)
        {
            line.SetPosition(index, startValue);
            return DOTween.To(() => line.GetPosition(index), val => line.SetPosition(index, val), endValue, duration);
        }

        // ReSharper disable once InconsistentNaming
        public static Tween DOCountDown(this TMPro.TextMeshProUGUI tmp, int start, int end, float duration)
        {
            return DOVirtual.Int(start, end, duration, x => tmp.text = x.ToString());
        }

        // ReSharper disable once InconsistentNaming
        public static Tween DOCountDown(this TMPro.TextMeshPro tmp, int start, int end, float duration)
        {
            return DOVirtual.Int(start, end, duration, x => tmp.text = x.ToString());
        }

        // ReSharper disable once InconsistentNaming
        public static Tween DOShaderColor(this Material material, string propertyName, Color endValue, float duration)
        {
            if (material == null)
            {
                Debug.LogError("Material is null!");
                return null;
            }

            return DOTween.To(() => material.GetColor(propertyName), color => material.SetColor(propertyName, color), endValue, duration);
        }
        
    }
}