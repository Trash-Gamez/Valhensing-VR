using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _VanHelsingVR.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

//using Random = UnityEngine.Random;

namespace _VanHelsingVR.Enemy
{
    public class FlyingEnemySpawner : MonoBehaviour
    {
        [SerializeField, AssetsOnly] private FlyingEnemyBehaviour enemyPrefab;
        [SerializeField] private List<WayPointManager> wayPointManagers;
        [SerializeField] private List<Transform> spawners;
        [SerializeField] private Transform enemyContainer;

        [Title("Time Rates")] 
        [SerializeField] private FloatRangeReference initialSpawnTime;
        [SerializeField] private FloatRangeReference spawnRate;

        private bool _isActive;
    
        private static Dictionary<WayPointManager, FlyingEnemyBehaviour> _wayPointsInUse = new Dictionary<WayPointManager, FlyingEnemyBehaviour>();

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

            var spawner = spawners[Random.Range(0, spawners.Count)];
            var enemyBehaviour = Instantiate(enemyPrefab, spawner.position, Quaternion.identity, enemyContainer);
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