using System;
using System.Threading.Tasks;
using CMCore.Managers;
using CMCore.Models;
using CMCore.Utilities;
using CMCore.Utilities.Extensions;
using Dreamteck.Splines;
using Unity.VectorGraphics;
using UnityEngine;

namespace CMCore.Behaviors.Object
{
    public class TubeBehavior : PrefabBehavior
    {
 
        private const float c_SplineScale = 0.05f;
        private const string c_BallPrefab = "BallBehavior";


  
        private Transfigure m_DefaultTransfigure =
            new Transfigure(new Vector3(0, 3, 6), new Quaternion(0, 0, 0, 0), new Vector3(1, 1, 1));

        private Vector3 _lastMousePosition;

        [SerializeField] private SplineComputer m_SplineComputer;
        [SerializeField] private SplineMesh m_SplineMesh;
        [SerializeField] private MeshFilter m_SplineMeshFilter;
        
        
        [SerializeField] private SplineComputer m_SplineComputerVisual;
        [SerializeField] private SplineMesh m_SplineMeshVisual;
        [SerializeField] private MeshFilter m_SplineMeshFilterVisual;

        

        [SerializeField] private MeshFilter m_PremeshMeshFilter;
        [SerializeField] private MeshCollider m_PremeshMeshCollider;


        [SerializeField] private Transform m_BallReleaserTransform;

        public Transform BallReleaserTransform => m_BallReleaserTransform;


        public override void ResetBehavior()
        {
            base.ResetBehavior();

            m_PremeshMeshFilter.gameObject.SetActive(false);
            m_SplineMeshFilter.gameObject.SetActive(false);

            transform.position = m_DefaultTransfigure.position;
            transform.rotation = GameManager.LevelManager.Current.TubeSvg == null
                ? m_DefaultTransfigure.rotation
                : Quaternion.Euler(0, 0, 180);
            transform.localScale = m_DefaultTransfigure.scale;
            

            if (GameManager.LevelManager.Current.TubeSvg == null)
            {
                m_PremeshMeshFilter.sharedMesh = GameManager.LevelManager.Current.PreMeshTube.m_Mesh;
                m_PremeshMeshFilter.transform.localPosition =
                    GameManager.LevelManager.Current.PreMeshTube.m_BaseLocalPosition;
                m_PremeshMeshCollider.sharedMesh = m_PremeshMeshFilter.sharedMesh;

                m_BallReleaserTransform.localPosition =
                    GameManager.LevelManager.Current.PreMeshTube.m_BallReleaserLocalPosition;
                m_BallReleaserTransform.localEulerAngles =
                    GameManager.LevelManager.Current.PreMeshTube.m_BallReleaserLocalEulerAngles;

                SpawnBalls(GameManager.LevelManager.Current.PreMeshTube.m_ContainerLocalPosition);
                m_PremeshMeshFilter.gameObject.SetActive(true);
            }
            else
            {
                m_SplineComputer.updateMode = SplineComputer.UpdateMode.AllUpdate;
                m_SplineComputerVisual.updateMode = SplineComputer.UpdateMode.AllUpdate;

                var svgContent = Extend.ReadSVG();
                var svgInfo = Extend.ParseSVGContent(svgContent);
                var points = GenerateMesh(svgInfo);

                m_SplineMeshFilter.gameObject.SetActive(true);
                
                Extend.OptimizeMesh(m_SplineMeshFilter.sharedMesh);
                Extend.OptimizeMesh(m_SplineMeshFilterVisual.sharedMesh);


                PlaceContainer(points[0]);
                PlaceReleaser(points[^1]);

                this.DelayedAction(() =>
                {
                    
                    m_SplineComputer.updateMode = SplineComputer.UpdateMode.None;
                    m_SplineComputerVisual.updateMode = SplineComputer.UpdateMode.None;
                }, 0.1f);
            }
        }

        private void PlaceReleaser(SplinePoint point)
        {
            var position = point.position;
            position.z = 6;
            m_BallReleaserTransform.position = position;

        }

        private void PlaceContainer(SplinePoint point)
        {
            var container = this.GetFromPool("TubeContainer", transform);

            var positionOfContainer = point.position;
            positionOfContainer.z = 6;
            container.transform.position = positionOfContainer;
            container.transform.eulerAngles = Vector3.zero;
            
            
            SpawnBalls(container.transform.localPosition);
        }

        private async void SpawnBalls(Vector3 spawnLocalPosition)
        {
            for (int i = 0; i < GameManager.LevelManager.Current.BallCount; i++)
            {
                var ballPrefab = this.GetFromPool(c_BallPrefab, transform);
                var ball = ballPrefab.GetComponent<BallBehavior>();
                ball.ResetBehavior();
                ball.Setup(spawnLocalPosition);


                await Task.Delay(1);
            }
        }

        private SplinePoint[] GenerateMesh(SVGParser.SceneInfo info)
        {
            var points = Extend.GenerateSplinePointsFromSVGInfo(info, c_SplineScale,
                GameManager.LevelManager.Current.SvgTubeExtraXOffset);

            m_SplineComputer.is2D = true;
            m_SplineComputer.SetPoints(points.ToArray());
            m_SplineMesh.GetChannel(0).GetMesh(0).flipFaces = true;
            m_SplineComputer.RebuildImmediate();
            
            m_SplineComputerVisual.is2D = true;
            m_SplineComputerVisual.SetPoints(points.ToArray());
            m_SplineComputerVisual.RebuildImmediate();
            
            m_SplineMesh.RebuildImmediate();
            m_SplineMeshVisual.RebuildImmediate();
            return points.ToArray();
        }

        private void Update() => ProcessRotationMovement();

        private void ProcessRotationMovement()
        {
            if (InputManager.MouseDown)
            {
                _lastMousePosition = Input.mousePosition;
            }

            if (!InputManager.MousePressing) return;

            var currentMousePosition = Input.mousePosition;
            var mouseDelta = currentMousePosition - _lastMousePosition;
            _lastMousePosition = currentMousePosition;
            var rotationAmount = mouseDelta.x * this.Settings().RotationSensitivity;
            var newRotation = transform.eulerAngles + new Vector3(0f, 0f, rotationAmount);
            transform.eulerAngles = newRotation;
        }
    }
}