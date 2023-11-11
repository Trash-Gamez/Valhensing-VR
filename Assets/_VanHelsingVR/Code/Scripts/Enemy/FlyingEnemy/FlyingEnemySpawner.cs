using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _VanHelsingVR.Health;
using UnityEngine;
using Random = UnityEngine.Random;
using Sirenix.OdinInspector;
using _VanHelsingVR.Utilities;

//using Random = UnityEngine.Random;

namespace _VanHelsingVR.Enemy
{
    public class FlyingEnemySpawner : MonoBehaviour
    {
        [SerializeField, AssetsOnly] private FlyingEnemyStateMachine enemyPrefab;
        [SerializeField] private List<WayPointManager> wayPointManagers;
        [SerializeField] private Transform spawnerYPos;
        [SerializeField] private Transform enemyContainer;

        [Title("Time Rates")] 
        [SerializeField] private FloatRangeReference initialSpawnTime;
        [SerializeField] private FloatRangeReference spawnRate;

        [Title("Config")] [SerializeField] private bool destroyEnemiesWithDeactivate;

        private bool _isActive;
        private bool _isRoom = true;
    
        private static Dictionary<WayPointManager, FlyingEnemyStateMachine> _wayPointsInUse = new Dictionary<WayPointManager, FlyingEnemyStateMachine>();

        private IEnumerator ActivateSpawnCor()
        {
            yield return new WaitForSeconds(initialSpawnTime.Value);
            SpawnEnemy();
            _recurrentEnemySpawn = StartCoroutine(RecurrentEnemySpawn());
        }

        private void SpawnEnemy()
        {
            Debug.Log("Se spawnea el enemigo");
            var possibleWayPoints = wayPointManagers.Where(manager => !_wayPointsInUse.ContainsKey(manager)).ToList();
            if (!possibleWayPoints.Any())
            {
                Debug.Log("No hay nada");
                _isRoom = false;
                return;
            }

            var wayPointManager = possibleWayPoints[Random.Range(0, possibleWayPoints.Count)];
            
            //spawnea al enemigo en la zona de abajo del jugador, pero en la posicion del primer waypoint para que solo suba directamente arriba
            var spawnPos = wayPointManager.GetFirst().position;
            spawnPos.y = spawnerYPos.position.y;
            
            var enemyBehaviour = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, enemyContainer);
            
            enemyBehaviour.wayPointManager = wayPointManager;
            
            //Esto se podria cambiar a que solo hay aun diferente tipo de contenedor
            enemyBehaviour.bulletContainer = enemyContainer;
            
            _wayPointsInUse.Add(wayPointManager, enemyBehaviour);
        }

        private void OnEnemyDead(FlyingEnemyStateMachine flyingEnemy)
        {
            if(!_wayPointsInUse.ContainsKey(flyingEnemy.wayPointManager)) return;
            _wayPointsInUse.Remove(flyingEnemy.wayPointManager);
            
            if(_recurrentEnemySpawn == null)
                _recurrentEnemySpawn = StartCoroutine(RecurrentEnemySpawn());
            _isRoom = true;
        }

        private Coroutine _recurrentEnemySpawn = null;
        private IEnumerator RecurrentEnemySpawn()
        {
            var rate = spawnRate.Value;
            
            Debug.Log("Recurrente en: " + rate);
            
            while (_isActive && _isRoom)
            {
                yield return new WaitForSeconds(rate);
                SpawnEnemy();
            }

            _recurrentEnemySpawn = null;
        }
        
        public void ActivateSpawn()
        {
            _isActive = true;
            StartCoroutine(ActivateSpawnCor());
        }

        public void DeactivateSpawn()
        {
            _isActive = false;
            if (!destroyEnemiesWithDeactivate) return;
            
            foreach (var hurtbox in enemyContainer.GetComponents<Hurtbox>())
            {
                hurtbox.ForceDeath();
            }
        }

        private void OnEnable()
        {
            FlyingEnemyStateMachine.OnEnemyDead += OnEnemyDead;
        }

        private void OnDisable()
        {
            FlyingEnemyStateMachine.OnEnemyDead -= OnEnemyDead;
        }
    }
}