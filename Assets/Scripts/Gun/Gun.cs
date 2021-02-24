using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Projectile projectile;
    public float rateOfFire = 10;   // Number of projectile per second
    public float muzzleVelocity = 35;
    public float projectileTimeToLive = 2;

    private float m_nextShotTimer;
    private float m_ShotTimeStep;
    private bool m_IsHoldShoot = false;

    private void Start()
    {
        m_ShotTimeStep = 1 / rateOfFire;
    }

    private void Update()
    {
        if (m_IsHoldShoot)
            Shoot();
    }

    public void SetHoldShoot(bool isHoldShoot)
    {
        m_IsHoldShoot = isHoldShoot;
    }

    public void Shoot()
    {
        if (Time.time > m_nextShotTimer)
        {
            m_nextShotTimer = Time.time + m_ShotTimeStep;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            newProjectile.speed = muzzleVelocity;
            newProjectile.timeOut = Time.time + projectileTimeToLive;
            newProjectile.vfx.SendEvent("OnPlay");
        }
    }
}
