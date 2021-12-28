using Mirror;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NetworkBehaviour
{
    [SerializeField]
    private float walkingSpeed = 200f;

    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private AIDestinationSetter destSetter;


    private void Start()
    {
        if (!isServer) return;
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        Debug.Log(player.name);
        destSetter.target = player.transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("ZOMBIE DAMAGE");
        }
    }
}
