using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _VanHelsingVR.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

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

        private bool _isActive;
    
        private static Dictionary<WayPointManager, FlyingEnemyStateMachine> _wayPointsInUse = new Dictionary<WayPointManager, FlyingEnemyStateMachine>();

        private IEnumerator ActivateSpawnCor()
        {
            yield return new WaitForSeconds(initialSpawnTime.Value);
            SpawnEnemy();
            StartCoroutine(RecurrentEnemySpawn());
        }

        private void SpawnEnemy()
        {
            var possibleWayPoints = wayPointManagers.Where(manager => !_wayPointsInUse.ContainsKey(manager)).ToList();
            if (!possibleWayPoints.Any()) return;
            
            var wayPointManager = possibleWayPoints[Random.Range(0, possibleWayPoints.Count)];
            
            //spawnea al enemigo en la zona de abajo del jugador, pero en la posicion del primer waypoint para que solo suba directamente arriba
            var spawnPos = wayPointManager.GetFirst().position;
            spawnPos.y = spawnerYPos.position.y;
            
            var enemyBehaviour = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, enemyContainer);
            enemyBehaviour.wayPointManager = wayPointManager;
        }

        private IEnumerator RecurrentEnemySpawn()
        {
            yield return null;
        }
        
        public void ActivateSpawn()
        {
            _isActive = true;
            StartCoroutine(ActivateSpawnCor());
        }

        public void DeactivateSpawn()
        {
            _isActive = false;
        }

    }
}