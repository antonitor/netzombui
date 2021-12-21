using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipedWeapon
{
    nothing = 0,
    beretta = 1,
    ak47 = 2
}

public class PlayerController : NetworkBehaviour
{
    //player movement
    private Vector2 movement;

    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private float moveSpeed = 200f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer model;

    //sprite flipX
    [SyncVar(hook = nameof(OnChangeLookingRight))]
    private bool lookingRight = false;

    private void OnChangeLookingRight(bool _, bool newLookingRightState)
    {
        model.flipX = newLookingRightState;
    }

    //Client chache
    private bool oldLookingRight = false;
    private bool oldWalkingValue = false;
    private bool oldBackwardsValue = false;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;

        PlayerInput();
        AnimateCharacter();

    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        if (movement.x != 0 && movement.y != 0)
        {
            rb2d.velocity = new Vector2(movement.x * Time.fixedDeltaTime * moveSpeed * 0.7f, movement.y * Time.fixedDeltaTime * moveSpeed * 0.7f);
        }
        else
        {
            rb2d.velocity = new Vector2(movement.x * Time.fixedDeltaTime * moveSpeed, movement.y * Time.fixedDeltaTime * moveSpeed);
        }
    }

    private void PlayerInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }


    private void AnimateCharacter()
    {
        bool lookingRightValue = movement.x > 0 ? true : movement.x < 0 ? false : oldLookingRight;
        if (oldLookingRight != lookingRightValue)
        {
            oldLookingRight = lookingRightValue;
            CmdUpdateFacingDir(lookingRightValue);
        }
        bool isWalkingValue = movement.sqrMagnitude != 0;
        if (oldWalkingValue != isWalkingValue)
        {
            oldWalkingValue = isWalkingValue;
            animator.SetBool("Walking", isWalkingValue);
        }
        bool isBackwardsValue = movement.y > 0 ? true : movement.y < 0 ? false : oldBackwardsValue;
        if (isBackwardsValue != oldBackwardsValue)
        {
            oldBackwardsValue = isBackwardsValue;
            animator.SetBool("Backwards", isBackwardsValue);
        }
    }


    [Command]
    private void CmdUpdateFacingDir(bool isLookingRight)
    {
        lookingRight = isLookingRight;
    }
}
