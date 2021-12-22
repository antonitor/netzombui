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

    [SerializeField]
    public float force = 1000f;

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    private void Start()
    {
        rb2d.AddForce(new Vector2(transform.forward.x, transform.forward.y) * force, ForceMode2D.Impulse);
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
        Debug.Log(coll.gameObject.GetComponent<Bullet>() != null);
        if (coll.gameObject.GetComponent<Bullet>() == null)
            NetworkServer.Destroy(gameObject);
    }
}
