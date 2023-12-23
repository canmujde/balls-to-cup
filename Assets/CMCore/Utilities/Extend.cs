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
        
        
        

       

        public static Vector2 RandomizePosition(Vector2 pos, float maxOffset)
        {
            var offsetX = Random.Range(-maxOffset, maxOffset);
            var offsetY = Random.Range(-maxOffset, maxOffset);
            var offsetZ = Random.Range(-maxOffset, maxOffset);

            var newPosition = new Vector2(
                pos.x + offsetX,
                pos.y + offsetY
            );

            return newPosition;
        }
       
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
            var queue = new Queue<Transform>();
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