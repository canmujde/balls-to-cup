using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CMCore.Managers;
using DG.Tweening;
using Dreamteck.Splines;
using Unity.VectorGraphics;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
// ReSharper disable InconsistentNaming

namespace CMCore.Utilities
{
    public static class Extend
    {
        
        
        public static string ReadSVG()
        {
            var svgPath = AssetDatabase.GetAssetPath(GameManager.LevelManager.Current.TubeSvg).Replace("Assets/", "");
            var fullPath = Path.Combine(Application.dataPath, svgPath);
            return File.ReadAllText(fullPath);
        }
        
        public static SVGParser.SceneInfo ParseSVGContent(string content)
        {
            var stringRead = new StringReader(content);
            return SVGParser.ImportSVG(stringRead);
        }


        public static List<SplinePoint> GenerateSplinePointsFromSVGInfo(SVGParser.SceneInfo info, float scaleFactor, float extraXOffset)
        {
            var sceneRoot = info.Scene.Root;
            var firstPath = sceneRoot.Children[0];
            var firstShape = firstPath.Shapes[0];
            var firstContour = firstShape.Contours[0];
            var segments = firstContour.Segments;
            var points = new List<SplinePoint>();


            foreach (var currentSegment in segments)
            {
                var pos = currentSegment.P0 * scaleFactor;
                var splinePoint = new SplinePoint(pos + Vector2.right*extraXOffset);

                points.Add(splinePoint);
            }

            if (points[0].position.y > points.Last().position.y)
            {
                var newList = new List<SplinePoint>();
                var offset = points[0].position.y;
                foreach (var splinePoint in points)
                {
                
                    var pos = splinePoint.position;

                    pos.y *= -1;
                
                
                    var abs = Mathf.Abs(offset);
                    pos.y += abs;
                
                    newList.Add(new SplinePoint(pos));
 
                }
                points = newList;
            }
            
            
            return RemoveClosePoints(points);
        }

        public static List<SplinePoint> RemoveClosePoints(List<SplinePoint> points)
        {
            for (int i = points.Count - 2; i >= 0; i--)
            {
                var distance = Vector3.Distance(points[i].position, points[i + 1].position);

                if (distance < 0.2f)
                {
                    points.RemoveAt(i + 1);
                }
            }

            return points;
        }

        public static void OptimizeMesh(Mesh mesh) => MeshUtility.Optimize(mesh);
        
        public static void Randomize(this ref Vector3 vector3, Vector3 min, Vector3 max)
        {
            vector3 = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));

        }
        
        
        

        // public static void AnimateImageFromWorldToUI(this Transform spawnAt, Canvas canvas, Ease ease,
        //     RectTransform target,
        //     Sprite sprite, float duration, bool popAtStart, float popPower, float startDelay, Action onComplete)
        // {
        //     var image = Array.Find(GameManager.UIManager.InGameUI.FloatingImages,
        //         image1 => image1.gameObject.activeSelf == false);
        //
        //     if (image && image.rectTransform)
        //         image.rectTransform.DOKill();
        //     image.transform.localScale = Vector3.one;
        //     image.sprite = sprite;
        //     image.gameObject.SetActive(true);
        //     image.rectTransform.anchoredPosition = GetUIPositionFromWorldPosition(canvas, spawnAt, Vector3.zero);
        //     image.rectTransform.sizeDelta = target.sizeDelta;
        //
        //     if (popAtStart)
        //         image.rectTransform.DOPunchScale(Vector3.one * popPower, 0.3f, 0, 0);
        //     image.rectTransform.DOMove(target.transform.position, duration).SetEase(ease).SetDelay(startDelay)
        //         .SetSpeedBased(false).OnComplete(() =>
        //         {
        //             image.gameObject.SetActive(false);
        //             onComplete?.Invoke();
        //         });
        // }

        public static void AnimateObjectiveIntroduce(this RectTransform firstRect, RectTransform targetRect, Ease ease,
            float duration, float delay, Action onComplete)
        {
            Canvas.ForceUpdateCanvases();
            firstRect.DOKill();
            firstRect.localScale = Vector3.one;

            firstRect.DOMove(targetRect.position, duration).SetEase(ease)
                .SetDelay(delay).OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
        }

        public static Vector2 CalculateAnchoredPosition(RectTransform target, RectTransform destinationParent)
        {
            // Gidecek olan objenin parent'ının ve kendisinin anchor'ları
            Vector2 sourceAnchorMin = target.parent.GetComponent<RectTransform>().anchorMin;
            Vector2 sourceAnchorMax = target.parent.GetComponent<RectTransform>().anchorMax;
            Vector2 sourceAnchorPivot = target.parent.GetComponent<RectTransform>().pivot;

            // Hedef objenin parent'ının anchorları
            Vector2 destAnchorMin = destinationParent.anchorMin;
            Vector2 destAnchorMax = destinationParent.anchorMax;
            Vector2 destAnchorPivot = destinationParent.pivot;

            // Gidecek olan objenin pivot ve anchor'larına göre hedef objeye götürecek offset hesaplanıyor
            Vector2 offset = new Vector2(
                (sourceAnchorPivot.x - destAnchorPivot.x) * destinationParent.rect.width,
                (sourceAnchorPivot.y - destAnchorPivot.y) * destinationParent.rect.height
            );

            // Gidecek olan objenin anchor değerleri ile offset hesabı yapılıyor
            Vector2 anchoredPosition = new Vector2(
                (sourceAnchorMin.x - destAnchorMin.x) * destinationParent.rect.width + offset.x,
                (sourceAnchorMin.y - destAnchorMin.y) * destinationParent.rect.height + offset.y
            );

            // Hedef objenin parent'ının anchor değerleri ile de hesap yapılıyor
            anchoredPosition -= new Vector2(
                destAnchorMin.x * destinationParent.rect.width,
                destAnchorMin.y * destinationParent.rect.height
            );

            anchoredPosition += destinationParent.pivot * destinationParent.rect.size;

            return anchoredPosition;
        }

        // public static void AnimateObjectiveComplete(this Transform spawnAt, Canvas canvas, Ease ease,
        //     RectTransform target,
        //     Sprite sprite, float duration, float startDelay, bool popUpTargetAtEnd, Action onStart, Action onComplete)
        // {
        //     var image = Array.Find(GameManager.UIManager.InGameUI.FloatingImages,
        //         image1 => image1.gameObject.activeSelf == false);
        //
        //     if (image && image.rectTransform)
        //         image.rectTransform.DOKill();
        //     image.transform.localScale = Vector3.one;
        //     image.sprite = sprite;
        //     image.gameObject.SetActive(true);
        //     image.rectTransform.anchoredPosition = GetUIPositionFromWorldPosition(canvas, spawnAt, Vector3.zero);
        //     image.rectTransform.sizeDelta = target.sizeDelta;
        //     image.transform.localScale = Vector3.zero;
        //     image.transform.DOScale(Vector3.one, 0.2f).OnComplete(() =>
        //     {
        //         image.rectTransform.DOMove(target.transform.position, duration).SetEase(ease).SetDelay(startDelay)
        //             .OnStart(() =>
        //             {
        //                 onStart?.Invoke();
        //             })
        //             .OnComplete(() =>
        //             {
        //                 if (popUpTargetAtEnd)
        //                 {
        //                     target.transform.DOKill();
        //                     target.transform.localScale = Vector3.one;
        //                     target.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f, 3, 1);
        //                 }
        //
        //                 image.gameObject.SetActive(false);
        //                 onComplete?.Invoke();
        //             });
        //     });
        // }

        public static void AnimateHammerMovement(this RectTransform image, Ease ease, RectTransform startFromRect,
            Transform worldObject, float speed, bool popAtStart,
            float popPower, Action onComplete)
        {
            image.DOKill();
            image.gameObject.SetActive(true);
            image.position = startFromRect.position;


            if (popAtStart)
                image.DOPunchScale(Vector3.one * popPower, 0.3f, 0, 0);
            image.DOAnchorPos(GetUIPositionFromWorldPosition(GameManager.UIManager.Canvas, worldObject, Vector3.zero),
                speed).SetEase(ease).OnComplete(
                () =>
                {
                    // image.gameObject.SetActive(false);
                    onComplete?.Invoke();
                });
        }

        public static void AnimateBombMovement(this RectTransform image, Ease ease, RectTransform startFromRect,
            Transform worldObject, float speed, bool popAtStart,
            float popPower, Action onComplete)
        {
            image.DOKill();
            image.gameObject.SetActive(true);
            image.position = startFromRect.position;


            if (popAtStart)
                image.DOPunchScale(Vector3.one * popPower, 0.3f, 0, 0);
            image.DOAnchorPos(GetUIPositionFromWorldPosition(GameManager.UIManager.Canvas, worldObject, Vector3.zero),
                speed).SetEase(ease).OnComplete(
                () =>
                {
                    // image.gameObject.SetActive(false);
                    onComplete?.Invoke();
                });
        }

        // public static void AnimateObjectiveComplete(this Vector3 pos, Canvas canvas, Ease ease,
        //     RectTransform target,
        //     Sprite sprite, float duration, float startDelay, bool popUpTargetAtEnd, Action onComplete)
        // {
        //     var image = Array.Find(GameManager.UIManager.InGameUI.FloatingImages,
        //         image1 => image1.gameObject.activeSelf == false);
        //
        //     if (image && image.rectTransform)
        //         image.rectTransform.DOKill();
        //     image.transform.localScale = Vector3.one;
        //     image.sprite = sprite;
        //     image.gameObject.SetActive(true);
        //     image.rectTransform.anchoredPosition = GetUIPositionFromWorldPosition(canvas, pos, Vector3.zero);
        //     image.rectTransform.sizeDelta = target.sizeDelta;
        //     image.transform.localScale = Vector3.zero;
        //     image.transform.DOScale(Vector3.one * 0.6f, 0.4f).OnComplete(() =>
        //     {
        //         image.rectTransform.DOMove(target.transform.position, duration).SetEase(ease).SetDelay(startDelay)
        //             .SetSpeedBased(false).OnComplete(() =>
        //             {
        //                 if (popUpTargetAtEnd)
        //                 {
        //                     target.transform.DOKill();
        //                     target.transform.localScale = Vector3.one;
        //                     target.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f, 3, 1);
        //                 }
        //
        //                 image.gameObject.SetActive(false);
        //                 onComplete?.Invoke();
        //             });
        //     });
        // }

        // public static async void AnimateImageFromWorldToUIMultiple(this Transform spawnAt, Canvas canvas,
        //     RectTransform target,
        //     Sprite sprite, float speed, bool popAtStart, float popPower, float sizeDelta, int count, Action onComplete)
        // {
        //     for (int i = 0; i < count; i++)
        //     {
        //         var startPosition = GetUIPositionFromWorldPosition(canvas, spawnAt, Vector3.zero);
        //
        //         startPosition = RandomizePosition(startPosition, 100);
        //         var image = Array.Find(GameManager.UIManager.InGameUI.FloatingImages,
        //             image1 => image1.gameObject.activeSelf == false);
        //
        //         if (image && image.rectTransform)
        //             image.rectTransform.DOKill();
        //         image.transform.localScale = Vector3.one;
        //         image.sprite = sprite;
        //         image.gameObject.SetActive(true);
        //         image.rectTransform.anchoredPosition = startPosition;
        //         image.rectTransform.sizeDelta = sizeDelta * Vector2.one;
        //
        //         if (popAtStart)
        //             image.rectTransform.DOPunchScale(Vector3.one * popPower, 0.3f, 0, 0);
        //         image.rectTransform.DOMove(target.transform.position, speed).SetDelay(count * 0.15f)
        //             .SetSpeedBased(false).OnComplete(() => { image.gameObject.SetActive(false); });
        //
        //         await Task.Delay(100);
        //     }
        //
        //     await Task.Delay(300);
        //     onComplete?.Invoke();
        //     GameManager.AudioManager.PlaySfx("Cubes turning into money_v1");
        // }


        public static Vector2 RandomizePosition(Vector2 pos, float maxOffset)
        {
            // Generate random offset values
            float offsetX = Random.Range(-maxOffset, maxOffset);
            float offsetY = Random.Range(-maxOffset, maxOffset);
            float offsetZ = Random.Range(-maxOffset, maxOffset);

            // Apply the offset to the initial position
            Vector2 newPosition = new Vector2(
                pos.x + offsetX,
                pos.y + offsetY
            );

            // Set the new position of the object
            return newPosition;
        }
        // public static void AnimateMerge(this Sprite sprite, RectTransform firstImageRect, RectTransform secondImageRect, float speed, bool popAtStart, float popPower, Action onComplete)
        // {
        //     var target = (firstImageRect.position + secondImageRect.position) / 2;
        //
        //
        //     var firstImage = Array.Find(GameManager.UIManager.inGameUI.FloatingImages,
        //         image1 => image1.gameObject.activeSelf == false);
        //     firstImage.rectTransform.DOKill();
        //     firstImage.transform.localScale = Vector3.one;
        //     firstImage.sprite = sprite;
        //     firstImage.gameObject.SetActive(true);
        //     firstImage.rectTransform.position = firstImageRect.position;
        //     firstImage.rectTransform.sizeDelta = firstImageRect.sizeDelta;
        //
        //     if (popAtStart)
        //         firstImage.rectTransform.DOPunchScale(Vector3.one * popPower, 0.3f, 0, 0);
        //     firstImage.rectTransform.DOMove(target, speed).SetSpeedBased(true).SetEase(Ease.InBack).SetDelay(0.2f).OnComplete(() =>
        //     {
        //         firstImage.gameObject.SetActive(false);
        //     });
        //
        //     var secondImage = Array.Find(GameManager.UIManager.inGameUI.FloatingImages,
        //         image1 => image1.gameObject.activeSelf == false);
        //
        //     secondImage.rectTransform.DOKill();
        //     secondImage.transform.localScale = Vector3.one;
        //     secondImage.sprite = sprite;
        //     secondImage.gameObject.SetActive(true);
        //     secondImage.rectTransform.position = secondImageRect.position;
        //     secondImage.rectTransform.sizeDelta = secondImageRect.sizeDelta;
        //     if (popAtStart)
        //         secondImage.rectTransform.DOPunchScale(Vector3.one * popPower, 0.3f, 0, 0);
        //
        //     secondImage.rectTransform.DOMove(target, speed).SetSpeedBased(true).SetEase(Ease.InBack).SetDelay(0.2f).OnComplete(() =>
        //     {
        //         VibrationsManager.Play(HapticPatterns.PresetType.LightImpact);
        //         secondImage.gameObject.SetActive(false);
        //         GameManager.UIManager.inGameUI.AnimateMergeParticle(target);
        //         onComplete?.Invoke();
        //     });
        // }

        // public static void AnimateFromUIToWorld(this Sprite sprite, Ease ease, RectTransform startFromRect,
        //     Transform worldObject, float sizeDelta, float speed, bool popAtStart,
        //     float popPower, Action onComplete)
        // {
        //     var img = Array.Find(GameManager.UIManager.InGameUI.FloatingImages,
        //         image1 => image1.gameObject.activeSelf == false);
        //     img.rectTransform.DOKill();
        //     img.transform.localScale = Vector3.one;
        //     img.sprite = sprite;
        //     img.gameObject.SetActive(true);
        //     img.rectTransform.position = startFromRect.position;
        //     img.rectTransform.sizeDelta = sizeDelta * Vector2.one;
        //
        //     if (popAtStart)
        //         img.rectTransform.DOPunchScale(Vector3.one * popPower, 0.3f, 0, 0);
        //     img.rectTransform
        //         .DOAnchorPos(GetUIPositionFromWorldPosition(GameManager.UIManager.Canvas, worldObject, Vector3.zero),
        //             speed).SetSpeedBased(false).SetEase(ease).SetDelay(1).OnComplete(
        //             () =>
        //             {
        //                 img.gameObject.SetActive(false);
        //                 onComplete?.Invoke();
        //             });
        // }


        // public static void AnimateUISpriteMovement(this Sprite sprite, Ease ease, RectTransform firstImageRect,
        //     RectTransform secondImageRect, Vector3 offset, Vector2 sizeDelta, float duration, bool popAtStart,
        //     float popPower, float delay, Action onComplete)
        // {
        //     var img = Array.Find(GameManager.UIManager.InGameUI.FloatingImages,
        //         image1 => image1.gameObject.activeSelf == false);
        //     img.rectTransform.DOKill();
        //     img.transform.localScale = Vector3.one;
        //     img.sprite = sprite;
        //     img.gameObject.SetActive(true);
        //     img.rectTransform.position = firstImageRect.position + offset;
        //     img.rectTransform.sizeDelta = sizeDelta;
        //     img.transform.localScale = Vector3.one;
        //
        //
        //     if (popAtStart)
        //         img.rectTransform.DOPunchScale(Vector3.one * popPower, 0.3f, 0, 0);
        //     img.rectTransform.DOMove(secondImageRect.position, duration).SetSpeedBased(false).SetEase(ease)
        //         .SetDelay(delay).OnComplete(() =>
        //         {
        //             img.gameObject.SetActive(false);
        //             onComplete?.Invoke();
        //         });
        // }
        //
        // public static async void AnimateUISpriteMovementMultiple(this Sprite sprite, Ease ease,
        //     RectTransform firstImageRect, RectTransform secondImageRect, float sizeDelta, float speed,
        //     bool popAtStart,
        //     float popPower, int count, Action onComplete)
        // {
        //     for (int i = 0; i < count; i++)
        //     {
        //         var img = Array.Find(GameManager.UIManager.InGameUI.FloatingImages,
        //             image1 => image1.gameObject.activeSelf == false);
        //         img.rectTransform.DOKill();
        //         img.transform.localScale = Vector3.one;
        //         img.sprite = sprite;
        //         img.gameObject.SetActive(true);
        //         img.rectTransform.position = firstImageRect.position;
        //         img.rectTransform.anchoredPosition = RandomizePosition(img.rectTransform.anchoredPosition, 100);
        //         img.rectTransform.anchoredPosition -= Vector2.up * 100;
        //         img.rectTransform.sizeDelta = sizeDelta * Vector2.one;
        //
        //         if (popAtStart)
        //             img.rectTransform.DOPunchScale(Vector3.one * popPower, 0.3f, 0, 0);
        //         img.rectTransform.DOMove(secondImageRect.position, speed).SetSpeedBased(false).SetEase(ease)
        //             .SetDelay(1f).OnComplete(() => { img.gameObject.SetActive(false); });
        //
        //         await Task.Delay(100);
        //     }
        //
        //     onComplete?.Invoke();
        // }

        public static Vector2 GetUIPositionFromWorldPosition(Canvas canvas, Transform worldTransform, Vector3 offset)
        {
            var screenPos = GameManager.CameraManager.MainCamera.WorldToScreenPoint(worldTransform.position + offset);
            var h = Screen.height;
            var w = Screen.width;
            var x = screenPos.x - ((float)w / 2);
            var y = screenPos.y - ((float)h / 2);
            var s = canvas.scaleFactor;

            return new Vector2(x, y) / s;
        }

        public static Vector2 GetUIPositionFromWorldPosition(Canvas canvas, Vector3 worldPosition, Vector3 offset)
        {
            var screenPos = GameManager.CameraManager.MainCamera.WorldToScreenPoint(worldPosition + offset);
            var h = Screen.height;
            var w = Screen.width;
            var x = screenPos.x - ((float)w / 2);
            var y = screenPos.y - ((float)h / 2);
            var s = canvas.scaleFactor;

            return new Vector2(x, y) / s;
        }


        public static Transform FindDeepChild(this Transform aParent, string aName)
        {
            Queue<Transform> queue = new Queue<Transform>();
            queue.Enqueue(aParent);
            while (queue.Count > 0)
            {
                var c = queue.Dequeue();
                if (c.name == aName)
                    return c;
                foreach (Transform t in c)
                    queue.Enqueue(t);
            }

            return null;
        }

        public static bool IsPointerOverUIObject()
        {
            var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
            {
                position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
            };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        public static void ChangeLayer(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
        }


        public static Bounds GetBoundsOfChildren(this Transform parent)
        {
            var renderers = parent.GetComponentsInChildren<Renderer>();
            var bounds = renderers[0].bounds;
            for (var i = 1; i < renderers.Length; ++i)
                bounds.Encapsulate(renderers[i].bounds);

            return bounds;
        }

        public static void Fit(this Camera camera, Bounds bounds, float offset)
        {
            float screenRatio = (float)Screen.width / (float)Screen.height;
            float targetRatio = bounds.size.x / bounds.size.y;

            if (screenRatio >= targetRatio)
            {
                Debug.Log("s > t");
                float differenceInSize = targetRatio / screenRatio;
                Debug.Log(differenceInSize);
                camera.orthographicSize = bounds.size.y / 2 + offset * 2;

                var cameraPos = camera.transform.position;
                cameraPos.x = bounds.center.x;
                cameraPos.y = bounds.center.y - 0.75f;

                camera.transform.position = cameraPos;
            }
            else
            {
                // Debug.Log("else");
                // Debug.Log("boundsss: "  + bounds);
                float differenceInSize = targetRatio / screenRatio;
                // Debug.Log(differenceInSize);
                camera.orthographicSize = bounds.size.y / 2 * differenceInSize + offset;

                var cameraPos = camera.transform.position;
                cameraPos.x = bounds.center.x;
                cameraPos.y = bounds.center.y;

                camera.transform.position = cameraPos;
            }
        }


        public static void CalculateOrthographicSize(this Camera camera, Bounds boundingBox, float offset)
        {
            var orthographicSize = camera.orthographicSize;

            Vector2 min = boundingBox.min;
            Vector2 max = boundingBox.max;

            var width = (max - min).x * offset;
            var height = (max - min).y * offset;

            if (width > height)
            {
                orthographicSize = Mathf.Abs(width) / camera.aspect / 2f;
            }
            else
            {
                orthographicSize = Mathf.Abs(height) / 2f;
            }

            camera.orthographicSize = Mathf.Max(orthographicSize, 0.01f);

            var cameraPos = camera.transform.position;
            cameraPos.x = boundingBox.center.x;

            camera.transform.position = cameraPos;
        }

        public static void FitToBounds(this Camera camera, Bounds bounds, float offset)
        {
            var screenRatio = (float)Screen.width / (float)Screen.height;
            var targetRatio = bounds.size.x / bounds.size.y;

            if (screenRatio >= targetRatio)
            {
                camera.orthographicSize = bounds.size.y / 2 + offset;
            }
            else
            {
                var differenceInSize = targetRatio / screenRatio;
                camera.orthographicSize = bounds.size.y / 2 * differenceInSize + offset;
            }

            var cameraPos = camera.transform.position;
            cameraPos.x = bounds.center.x;

            camera.transform.position = cameraPos;
        }
    }
}