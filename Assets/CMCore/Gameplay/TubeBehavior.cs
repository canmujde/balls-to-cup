using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CMCore.Behaviors.Object;
using CMCore.Contracts;
using CMCore.Managers;
using CMCore.Utilities;
using Dreamteck.Splines;
using Dreamteck.Splines.Editor;
using Sirenix.OdinInspector;
using Unity.VectorGraphics;
using UnityEditor;
using UnityEngine;
using MeshUtility = UnityEditor.MeshUtility;
using Object = UnityEngine.Object;

public class TubeBehavior : PrefabBehavior
{
    //constant
    private const float c_SplineScale = 0.05f;
    
    
    //mutable
    [SerializeField] private SplineComputer m_SplineComputer;
    [SerializeField] private SplineMesh m_SplineMesh;
    [SerializeField] private MeshFilter m_MeshFilter;
    
    public Transform sphere;

    
    public override void ResetBehavior()
    {
        base.ResetBehavior();
        var svgContent = Extend.ReadSVG();
        var svgInfo = Extend.ParseSVGContent(svgContent);
        var mesh = GenerateMesh(svgInfo);
        
        
        Extend.OptimizeMesh(mesh);
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
    
    private Mesh GenerateMesh(SVGParser.SceneInfo info)
    {

        var points = Extend.GenerateSplinePointsFromSVGInfo(info, c_SplineScale);
        
        m_SplineComputer.is2D = true;
        m_SplineComputer.SetPoints(Array.Empty<SplinePoint>());
        m_SplineComputer.SetPoints(points.ToArray());
        m_SplineMesh.RebuildImmediate();
        m_SplineComputer.RebuildImmediate();
        
        sphere.position = points[^1].position;

        return Dreamteck.MeshUtility.Copy(m_MeshFilter.sharedMesh);
    }

    

}