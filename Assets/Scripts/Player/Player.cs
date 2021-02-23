using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class Player : MonoBehaviour
{
    public float speed = 10;

    public LineOfSightRenderer lightOfSightRenderer;
    public PlayerInput playerInput;

    private LineRenderer m_LineRenderer;
    private Camera m_Camera;
    private Plane m_groundPlane;
    private Vector2 m_velocity;
    private Vector3 m_lookAtPos;

    private void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_Camera = Camera.main;
        m_groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    public void OnActionMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        m_velocity = movement * speed;
    }

    public void OnActionLook(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = context.ReadValue<Vector2>();
        Ray ray = m_Camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 0));
        float rayHitDistance;
        if (m_groundPlane.Raycast(ray, out rayHitDistance))
        {
            m_lookAtPos = ray.GetPoint(rayHitDistance);
            transform.LookAt(new Vector3(m_lookAtPos.x, transform.position.y, m_lookAtPos.z));
        }
    }

    private void Update()
    {
        Debug.DrawLine(m_Camera.transform.position, m_lookAtPos, Color.red);
    }

    private void FixedUpdate()
    {
        if (m_velocity.x != 0 || m_velocity.y != 0)
        {
            Vector3 velocity = new Vector3(m_velocity.x, 0, m_velocity.y);
            transform.position = transform.position + velocity * Time.fixedDeltaTime;
        }

        //    RaycastHit rayHitInfo;
        //    if (Physics.Raycast(ray, out rayHitInfo, m_TerrainMask))
        //    {
        //        Vector2 fromPosition = new Vector2(transform.position.x, transform.position.z);
        //        Vector2 toPosition = new Vector2(rayHitInfo.point.x, rayHitInfo.point.z);
        //        Vector2 target = (toPosition - fromPosition).normalized * 100;
        //        toPosition = fromPosition + target;
        //        lightOfSightRenderer.DrawLineOfSight(m_LineRenderer, transform.position, new Vector3(toPosition.x, transform.position.y, toPosition.y));
        //    }
    }
}
