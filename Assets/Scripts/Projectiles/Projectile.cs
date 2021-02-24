using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float timeOut;
    public VisualEffect vfx;

    private void FixedUpdate()
    {
        if (Time.time > timeOut)
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }
}
