using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }
}
