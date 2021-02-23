using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Line Of Sight Renderer")]
public class LineOfSightRenderer : ScriptableObject
{
    public void DrawLineOfSight(LineRenderer lineRenderer, Vector3 fromPosition,  Vector3 toPosition)
    {
        lineRenderer.SetPosition(0, fromPosition);
        lineRenderer.SetPosition(1, toPosition);
    }
}
