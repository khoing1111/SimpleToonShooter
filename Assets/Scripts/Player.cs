using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class Player : MonoBehaviour
{
    public LineOfSightRenderer lightOfSightRenderer;
    public Camera mainCamera;

    private LineRenderer m_LineRenderer;
    private LayerMask m_TerrainMask;

    private void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_TerrainMask = LayerMask.GetMask("Main Terrain");
    }

    private void FixedUpdate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit rayHitInfo;
        if (Physics.Raycast(ray, out rayHitInfo, m_TerrainMask))
        {
            Vector2 fromPosition = new Vector2(transform.position.x, transform.position.z);
            Vector2 toPosition = new Vector2(rayHitInfo.point.x, rayHitInfo.point.z);
            Debug.Log(toPosition);
            Vector2 target = (toPosition - fromPosition).normalized * 100;
            toPosition = fromPosition + target;
            lightOfSightRenderer.DrawLineOfSight(m_LineRenderer, transform.position, new Vector3(toPosition.x, transform.position.y, toPosition.y));
        }
    }
}
