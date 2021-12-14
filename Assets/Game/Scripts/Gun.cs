using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public int bullets = 0;
    public bool magazine = true;
    public int magazineCapacity = 7;
    public float reloadTime = 2f;
    public bool automatic = true;
    public Transform firePoint;
    public ParticleSystem fireParticleSystem;
    public ParticleSystem shellParticleSystem;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    public override void Attack()
    {
        fireParticleSystem.Play();
        Vector2 direction = RadianToVector2(rb2d.rotation * Mathf.Deg2Rad);
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, range);

        Debug.DrawRay(firePoint.position, direction, Color.red, 0.5f);

        if (hit)
        {
            Debug.Log(hit.transform.name);
        }
        shellParticleSystem.Play();
    }

    private Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
}
