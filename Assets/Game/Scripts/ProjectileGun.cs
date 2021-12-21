using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : Weapon
{
   
    [SerializeField] 
    private GameObject _bulletPrefab;

    [SerializeField]
    private Transform _shootingPoint;

    [SerializeField]
    private float _shootDelay = 0.2f;

    private float _nextShootTime;


    private void Shoot()
    {
        _nextShootTime = Time.time + _shootDelay;
        Instantiate(_bulletPrefab, _shootingPoint.position, _shootingPoint.rotation);
    }

    private bool CanShoot()
    {
        return Time.time >= _nextShootTime;
    }
}
