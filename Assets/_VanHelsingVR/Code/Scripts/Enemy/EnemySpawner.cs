using System;
using System.Collections.Generic;
using UnityEngine;
using _VanHelsingVR.Enemy;

using Sirenix.OdinInspector;

public class EnemySpawner : MonoBehaviour
{
    public static event Action<List<Enemy>> OnSpawnPassed;
    [SerializeField, AssetsOnly] private List<Enemy> enemies;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        OnSpawnPassed?.Invoke(enemies);
        Destroy(gameObject);
    }
}
