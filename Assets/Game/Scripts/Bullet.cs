using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField]
    private float destroyAfter = 2f;

    [SerializeField]
    public Rigidbody2D rb2d;

    private int _damage = 1;

    [SerializeField]
    public float force = 1000f;

    private WeaponHandler _weaponHandler;

    private bool _triggered = false;

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    private void Start()
    {
        rb2d.AddForce(transform.right * force * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    // ServerCallback because we don't want a warning if OnTriggerEnter is
    // called on the client
    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (_triggered) return;
        _triggered = true;

        Enemy enemy = coll.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            Debug.Log("ENEMY HIT");
            enemy.GetComponent<Health>().TakeDamage(_damage, _weaponHandler.gameObject.GetComponent<PlayerController>().PlayerNumber);
        }
        if (coll.gameObject.GetComponent<Bullet>() == null)
            NetworkServer.Destroy(gameObject);
    }

    public void SetWeaponHandler(WeaponHandler weaponHandler) => _weaponHandler = weaponHandler;

    public void SetBulletDamage(int damage) => _damage = damage;
}
