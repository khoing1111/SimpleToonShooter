using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[ExecuteInEditMode]
public class Player : MonoBehaviour
{
    public float speed = 10;

    public PlayerInput playerInput;
    public Gun startingGun;

    private Camera m_Camera;
    private Plane m_GroundPlane;
    private Vector2 m_Velocity;
    private class Aim
    {
        public Vector3 pos;
        public bool changed = false;
    }

    private Aim m_Aim = new Aim();

    private Gun m_Gun;
    private Transform m_RightHand;

    private void Start()
    {
        m_Camera = Camera.main;
        m_GroundPlane = new Plane(Vector3.up, Vector3.zero);

        m_RightHand = transform.Find("RightHand");
        for (int index = 0; index < m_RightHand.childCount; index++)
            DestroyImmediate(m_RightHand.GetChild(index).gameObject);

        EquipGun(startingGun);
    }

    public void OnActionMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        m_Velocity = movement * speed;
    }

    public void OnActionLook(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = context.ReadValue<Vector2>();
        Ray ray = m_Camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 0));
        float rayHitDistance;
        if (m_GroundPlane.Raycast(ray, out rayHitDistance))
        {
            m_Aim.changed = true;
            m_Aim.pos = ray.GetPoint(rayHitDistance);
        }
    }

    public void OnActionShoot(InputAction.CallbackContext context)
    {
        if (context.interaction is TapInteraction && context.performed)
        {
            m_Gun.Shoot();
        }
        else if (context.interaction is HoldInteraction)
        {
            m_Gun.SetHoldShoot(context.started || context.performed);
        }
        
    }

    private void Update()
    {
        //Debug.DrawLine(m_Camera.transform.position, m_LookAtPos, Color.red);
    }

    private void FixedUpdate()
    {
        if (m_Velocity.x != 0 || m_Velocity.y != 0)
        {
            Vector3 velocity = new Vector3(m_Velocity.x, 0, m_Velocity.y);
            transform.position = transform.position + velocity * Time.fixedDeltaTime;
        }

        if (m_Aim.changed)
        {
            transform.LookAt(new Vector3(m_Aim.pos.x, transform.position.y, m_Aim.pos.z));
            m_Aim.changed = false;
        }
    }

    private void EquipGun(Gun gun)
    {
        if (m_Gun != null)
            Destroy(m_Gun.gameObject);

        m_Gun = Instantiate(gun, m_RightHand.position, m_RightHand.rotation) as Gun;
        m_Gun.name = "EquipedGun";
        m_Gun.transform.parent = m_RightHand;
    }
}
