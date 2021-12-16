using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Weapon : NetworkBehaviour
{
    public Rigidbody2D rb2d;
    public float damage;
    public float range = 300f;
    public SpriteRenderer spriteRenderer;


    private void Update()
    {
        Debug.Log("WEAPON ROTATION" + rb2d.rotation);
    }

    public void Equip()
    {
        Debug.Log("Equiped " + gameObject.name);
    }   
    public void Unequip()
    {
        Debug.Log("Un-Equiped " + gameObject.name);
    }

    public virtual void Attack()
    {
        Debug.Log("ATTACK!");
    }


}
