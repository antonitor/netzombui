using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : NetworkBehaviour
{
    private int selectedWeaponLocal = 1;
    private Weapon activeWeapon;
    private float weaponCooldownTime;

    private SceneScript sceneScript;

    [SerializeField]
    private Weapon[] weaponArray;

    [SyncVar(hook = nameof(OnWeaponChanged))]
    public int activeWeaponSynced = 1;
    private void OnWeaponChanged(int _Old, int _New)
    {
        if (0 < _Old && _Old < weaponArray.Length && weaponArray[_Old] != null)
            weaponArray[_Old].gameObject.SetActive(false);
        if (0 < _New && _New < weaponArray.Length && weaponArray[_New] != null)
        {

            weaponArray[_New].gameObject.SetActive(true);
            activeWeapon = weaponArray[activeWeaponSynced].GetComponent<Weapon>();
            if (isLocalPlayer)
                sceneScript.UIAmmo(activeWeapon.weaponAmmo);
        }
    }


    [SerializeField]
    private GameObject rightHand;

    //to capture mouse prosition
    private Camera cam;

    private void Awake()
    {
        sceneScript = GameObject.Find("SceneReference").GetComponent<SceneReference>().sceneScript;

        foreach (var item in weaponArray)
            if (item != null)
                item.gameObject.SetActive(false);

        if (selectedWeaponLocal < weaponArray.Length && weaponArray[selectedWeaponLocal] != null)
        {
            activeWeapon = weaponArray[selectedWeaponLocal].GetComponent<Weapon>();
            sceneScript.UIAmmo(activeWeapon.weaponAmmo);
        }
    }

    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwapWeapon();
        }
        if (0 < activeWeaponSynced && activeWeaponSynced < weaponArray.Length)
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDirecction = mousePosition - new Vector2(transform.position.x, transform.position.y);
            float weaponAngle = Mathf.Atan2(lookDirecction.y, lookDirecction.x) * Mathf.Rad2Deg;
            rightHand.transform.rotation = Quaternion.Euler(0, 0, weaponAngle);

            if (Input.GetButtonDown("Fire1")) //Fire1 is mouse 1st click
            {
                if (activeWeapon && Time.time > weaponCooldownTime && activeWeapon.weaponAmmo > 0)
                {
                    weaponCooldownTime = Time.time + activeWeapon.weaponCooldown;
                    activeWeapon.weaponAmmo -= 1;
                    sceneScript.UIAmmo(activeWeapon.weaponAmmo);
                    CmdShootRay();
                }
            }
        }

    }


    [Command]
    void CmdShootRay()
    {
        RpcFireWeapon();
    }

    [ClientRpc]
    void RpcFireWeapon()
    {
        //bulletAudio.Play(); muzzleflash  etc
        Bullet bullet = Instantiate(activeWeapon.bulletPrefab, activeWeapon.weaponfirePosition.position, activeWeapon.weaponfirePosition.rotation);
        bullet.SetWeaponHandler(this);
        bullet.SetBulletDamage(activeWeapon.Damage);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * activeWeapon.bulletSpeed;
        Destroy(bullet, activeWeapon.bulletLife);
    }

     private void SwapWeapon()
    {
        selectedWeaponLocal += 1;
        if (selectedWeaponLocal > weaponArray.Length)
            selectedWeaponLocal = 1;

        CmdChangeActiveWeapon(selectedWeaponLocal);
    }

    [Command]
    public void CmdChangeActiveWeapon(int newIndex)
    {
        activeWeaponSynced = newIndex;
    }
}
