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
    public Weapon weapon;
    public Camera cam;
    Vector2 mousePosition;

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        animateCharacter();

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

        Vector2 lookDirecction = mousePosition - weapon.rb2d.position;
        float angle = Mathf.Atan2(lookDirecction.y, lookDirecction.x) * Mathf.Rad2Deg;
        weapon.rb2d.rotation = angle;

        Vector2 offsetVector = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        Vector2 weaponPoss = offsetVector * weapon.rotationOffset + rb2d.position;
        weapon.rb2d.position = weaponPoss;
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
