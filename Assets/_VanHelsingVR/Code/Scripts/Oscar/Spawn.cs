using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _VanHelsingVR.Enemy;

public class Spawn : MonoBehaviour
{
    [SerializeField] private FlyingEnemySpawner batSpawner;
    private void OnTriggerEnter(Collider other)
    {
        batSpawner.Spawn();
    }
}
