using System;
using System.Collections.Generic;
using _VanHelsingVR.TESTS.IA;
using RacTools.Variables;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private bool pairSpawns;
        [SerializeField] private VariableReference<Transform> playerTransform;
        
        private Queue<Enemy> _enemyQueue = new Queue<Enemy>();

        private bool _enemyIsSpawned;
        private IDisposable _enemyDestroySubscription = null;
        
        private void AddEnemies(List<Enemy> enemiesToAdd)
        {
            for (int i = 0; i < enemiesToAdd.Count; i++)
            {
                var isPair = i % 2 == 0;
                if (isPair && pairSpawns)
                {
                    _enemyQueue.Enqueue(enemiesToAdd[i]);
                    continue;
                }

                if (!isPair && !pairSpawns)
                {
                    _enemyQueue.Enqueue(enemiesToAdd[i]);
                }
            }
        }

        private void Update()
        {
            if(_enemyQueue.Count > 0 && !_enemyIsSpawned)
                SpawnEnemy(_enemyQueue.Dequeue());
        }

        private void SpawnEnemy(Enemy enemyToSpawn)
        {
            _enemyIsSpawned = true;
            var enemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity, transform);
            enemy.GetComponent<TEST_EnemyStateMachine>().Target = playerTransform.Value;
            _enemyDestroySubscription = enemy.OnDestroyAsObservable().Subscribe(EnemyDestroyed);
        }

        private void EnemyDestroyed(Unit unit)
        {
            _enemyIsSpawned = false;
            _enemyDestroySubscription.Dispose();
            _enemyDestroySubscription = null;
        }

        private void OnEnable()
        {
            EnemySpawner.OnSpawnPassed += AddEnemies;
        }

        private void OnDisable()
        {
            EnemySpawner.OnSpawnPassed -= AddEnemies;
        }
    }
}