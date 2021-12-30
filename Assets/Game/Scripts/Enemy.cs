using Mirror;
using Pathfinding;
using System;
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
    private SpriteRenderer model;

    [SerializeField]
    private AIDestinationSetter AIDestinationSetter;

    [SerializeField]
    private AIPath AIPath;

    [SyncVar(hook = nameof(OnChangeLookingRight))]
    private bool enemyLookingRight = false;

    private void OnChangeLookingRight(bool _, bool newLookingRightState)
    {
        model.flipX = newLookingRightState;
    }


    private void Start()
    {
        if (!isServer) return;
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        AIDestinationSetter.target = player.transform;
    }

    private void Update()
    {
        if (!isServer) return;
        if (AIPath.desiredVelocity.x > .01f && !enemyLookingRight)
        {
            enemyLookingRight = true;
        }
        if (AIPath.desiredVelocity.x < .01f && enemyLookingRight)
        {
            enemyLookingRight = false;
        }

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
