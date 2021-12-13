using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public Rigidbody2D rb2d;
    public float moveSpeed = 5f;
    Vector2 movement;
    void HandleMovement()
    {
        if(isLocalPlayer)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");     
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
