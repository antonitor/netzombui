using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : NetworkBehaviour
{
    [SerializeField]
    private GameObject rightHand;

    [SerializeField]
    private List<Weapon> weaponList;

    [SerializeField]
    private Weapon weapon;

    [SyncVar(hook = nameof(OnChangeWeapon))]
    public EquipedWeapon equipedWeapon;

    //to capture mouse prosition
    private Camera cam;

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
        if (equipedWeapon != EquipedWeapon.nothing)
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDirecction = mousePosition - new Vector2(transform.position.x, transform.position.y);
            float weaponAngle = Mathf.Atan2(lookDirecction.y, lookDirecction.x) * Mathf.Rad2Deg;
            rightHand.transform.rotation = Quaternion.Euler(0, 0, weaponAngle);
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CmdFire();
            }
        }
        
    }

    [Command]
    private void CmdFire()
    {
        GameObject projectile = Instantiate(weapon.bulletPrefab.gameObject, weapon.fireSpawnPoint.position, rightHand. transform.rotation);
        NetworkServer.Spawn(projectile);
        RpcOnFire();
    }

    [ServerCallback]
    private void RpcOnFire()
    {
        //animator fire
    }

    private void OnChangeWeapon(EquipedWeapon oldEquippedWeapon, EquipedWeapon newEquippedWeapon)
    {
        StartCoroutine(ChangeWeapon(newEquippedWeapon));
    }

    private IEnumerator ChangeWeapon(EquipedWeapon newEquippedWeapon)
    {
        while (rightHand.transform.childCount > 0)
        {
            Destroy(rightHand.transform.GetChild(0).gameObject);
            yield return null;
        }

        switch (newEquippedWeapon)
        {
            case EquipedWeapon.beretta:
                weapon = Instantiate(weaponList[0], rightHand.transform);
                break;
            case EquipedWeapon.ak47:
                weapon = Instantiate(weaponList[1], rightHand.transform);
                break;
        }
    }


    private void SwapWeapon()
    {
        switch (equipedWeapon)
        {
            case EquipedWeapon.nothing:
                CmdChangeEquipedWeapon(EquipedWeapon.beretta);
                break;
            case EquipedWeapon.beretta:
                CmdChangeEquipedWeapon(EquipedWeapon.ak47);
                break;
            case EquipedWeapon.ak47:
                CmdChangeEquipedWeapon(EquipedWeapon.beretta);
                break;

        }
    }

    [Command]
    private void CmdChangeEquipedWeapon(EquipedWeapon selectedWeapon)
    {
        equipedWeapon = selectedWeapon;
    }
}
