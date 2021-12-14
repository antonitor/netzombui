using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public Rigidbody2D rb2d;
    public float moveSpeed = 200f;
    public Animator animator;
    float upDown = -1f;
    Vector2 movement;

    private void Update()
    {
        if (isLocalPlayer)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animateCharacter();
        }
    }

    private void FixedUpdate()
    {
        if (movement.x != 0 && movement.y != 0)
        {
            rb2d.velocity = new Vector2(movement.x * Time.fixedDeltaTime * moveSpeed * 0.7f, movement.y * Time.fixedDeltaTime * moveSpeed * 0.7f);
        }
        else
        {
            rb2d.velocity = new Vector2(movement.x * Time.fixedDeltaTime * moveSpeed, movement.y * Time.fixedDeltaTime * moveSpeed);
        }
    }

    void animateCharacter()
    {
        animator.SetFloat("Speed", movement.sqrMagnitude);

        Debug.Log("SCALE X" + transform.localScale.x);

        if (movement.y > 0)
        {
            upDown = 1;
        }
        if (movement.y < 0)
        {
            upDown = -1;
        }

        if (movement.x > 0)
        {
            if (upDown < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            if (upDown > 0)
                transform.localScale = new Vector3(1, 1, 1);
        }
        if (movement.x < 0)
        {
            if (upDown < 0)
                transform.localScale = new Vector3(1, 1, 1);
            if (upDown > 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
        animator.SetFloat("Vertical", upDown);
    }
}
