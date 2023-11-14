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

    [SerializeField, Range(1,3)] private int numberEnemies;

    private void Start()
    {
        _spawner ??= FindObjectOfType<FlyingEnemySpawner>();
    }

    private void Logic()
    {
        if (isActive)
        {
            AudioManager.Instance.SwampMusic("GameplayMusic01");
            _spawner.ActivateSpawn();
        }
        else
        {
            AudioManager.Instance.SwampMusic("GameplayMusic03");
            _spawner.DeactivateSpawn();
        }

        Destroy(this);
    }

    private void OnCollisionEnter(Collision other)
    {
       
        if (!other.gameObject.CompareTag("Platform")) return;
        Logic();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!other.gameObject.CompareTag("Platform")) return;
        Logic();
    }
}
