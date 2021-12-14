using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (SpriteRenderer))]
public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb2d;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    public float damage;
    public float range = 300f;
    public float rotationOffset = 5f;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
