using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : NetworkBehaviour
{
    [SerializeField]
    private float startDelay = 10f;

    float _nextSpawnTime;

    [SerializeField]
    private float delay = 2f;

    [SerializeField]
    private Enemy enemyPrefab;

    private void Start()
    {
        _nextSpawnTime = Time.time + startDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer) return;

        if (ShouldSpawn())
        {
            Spawn();
        }

    }

    private void Spawn()
    {
        _nextSpawnTime = Time.time + delay;
        Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        NetworkServer.Spawn(enemy.gameObject);
    }

    private bool ShouldSpawn()
    {
        return Time.time >= _nextSpawnTime;
    }
}
