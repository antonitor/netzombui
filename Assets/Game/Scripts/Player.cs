using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public Rigidbody2D rb2d;
    public float moveSpeed = 200f;
    public Animator animator;
    public List<GameObject> weaponList;
    public SpriteRenderer model;
    float upDown = -1f;
    Vector2 movement;
    Weapon weapon;
    Camera cam;
    Vector2 mousePosition;

    private void Start()
    {
        cam = Camera.main;
        CmdSpawnWeapon(0);
    }

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
        if (weapon)
        {
            Vector2 lookDirecction = mousePosition - weapon.rb2d.position;
            float angle = Mathf.Atan2(lookDirecction.y, lookDirecction.x) * Mathf.Rad2Deg;
            Debug.Log("ANGLE " + angle);
            weapon.rb2d.rotation = angle;
            weapon.rb2d.position = rb2d.position;
            animateWeapon(angle);
        }

    }

    [Command]
    void CmdSpawnWeapon(int weapNum)
    {
        GameObject weaponPrefab = Instantiate(weaponList[weapNum], transform);
        weapon = weaponPrefab.GetComponent<Weapon>();
        weapon.transform.parent = transform;
        NetworkServer.Spawn(weaponPrefab);
    }

    void animateWeapon(float angle)
    {
        if (angle > 0)
        {
            weapon.spriteRenderer.sortingOrder = -1;
        }
        else
        {
            weapon.spriteRenderer.sortingOrder = 1;
        }
        if (angle > -90 && angle < 90)
        {
            weapon.spriteRenderer.flipY = false;
        }
        else
        {
            weapon.spriteRenderer.flipY = true;
        }
    }

    void animateCharacter()
    {
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //Debug.Log("SCALE X" + transform.localScale.x);

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
                //model.transform.localScale = new Vector3(-1, 1, 1);
                model.flipX = true;
            if (upDown > 0)
                //model.transform.localScale = new Vector3(1, 1, 1);
                model.flipX = false;
        }
        if (movement.x < 0)
        {
            if (upDown < 0)
                //model.transform.localScale = new Vector3(1, 1, 1);
                model.flipX = false;
            if (upDown > 0)
                //model.transform.localScale = new Vector3(-1, 1, 1);
                model.flipX = true;
        }
        animator.SetFloat("Vertical", upDown);
    }
}
