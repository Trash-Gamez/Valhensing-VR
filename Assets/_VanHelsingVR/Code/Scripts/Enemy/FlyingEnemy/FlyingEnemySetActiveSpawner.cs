using System;
using System.Collections;
using System.Collections.Generic;
using _VanHelsingVR.Enemy;
using ModestTree;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlyingEnemySetActiveSpawner : MonoBehaviour
{
    private static FlyingEnemySpawner _spawner;
    [SerializeField] private bool isActive = false;

    private void Start()
    {
        _spawner ??= FindObjectOfType<FlyingEnemySpawner>();
    }

    private void Logic()
    {
        if (isActive)
        {
            Debug.Log("Activated Spawner");
            _spawner.ActivateSpawn();
        }
        else
        {
            Debug.Log("Deactivated Spawner");
            _spawner.DeactivateSpawn();
        }

        Destroy(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("activate spawner collision: " + other.gameObject.name);
        if (!other.gameObject.CompareTag("Platform")) return;
        Logic();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger spawner collision: " + other.gameObject.name);
        if (!other.gameObject.CompareTag("Platform")) return;
        Logic();
    }
}
