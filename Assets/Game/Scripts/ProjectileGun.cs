using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : Weapon
{
    float _nextShootTime;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _shootingPoint;
    [SerializeField] float _shootDelay = 0.2f;


    // Update is called once per frame
    void Update()
    {
        if (CanShoot())
            Shoot();
    }

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
