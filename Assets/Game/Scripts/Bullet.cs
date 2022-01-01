using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb2d;

    private int _damage = 1;

    private WeaponHandler _weaponHandler;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Enemy enemy = collision.collider.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.GetComponent<Health>().TakeDamage(collision.transform.position ,_damage, _weaponHandler.gameObject.GetComponent<PlayerController>().PlayerNumber);
        }
    }

    public void SetWeaponHandler(WeaponHandler weaponHandler) => _weaponHandler = weaponHandler;

    public void SetBulletDamage(int damage) => _damage = damage;
}
