using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RacTools.RuntimeSet;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Range = _VanHelsingVR.Utilities.Range;

namespace _VanHelsingVR.Enemy
{
    public class FlyingEnemySpawner : MonoBehaviour
    {
        [SerializeField] private RuntimeSet<Transform> wayPoints;
        [SerializeField] private FlyingEnemy flyingEnemy;
        [SerializeField] private Range secondsForRandomize;
        
        private Dictionary<Transform, FlyingEnemy> wayPointsTaken = new();

        private int _enemyCount = 0;

        private void Start()
        {
            StartCoroutine(ChangeWaypoints());
        }
        
        // Spawnea un enemigo, si todos los waypoints estan usados no spawnea ninguno
        [ContextMenu("Spawn Enemy")]
        public void Spawn()
        {
            if (_enemyCount >= wayPoints.Set.Count) return;

            var wayPointsTemp = wayPoints.Set
                .Where(wayPoint => !wayPointsTaken.ContainsKey(wayPoint))
                .ToList();
            
            var appearWayPoint = wayPointsTemp[Random.Range(0, wayPointsTemp.Count)];
            var enemy = Instantiate(flyingEnemy, appearWayPoint.position, Quaternion.identity, transform);
            enemy.currentWaypoint = appearWayPoint;
            
            wayPointsTaken.Add(appearWayPoint, enemy);
            _enemyCount++;
        }
        
        //Hace un cambio de waypoints en todos los enemigos instanciados en los waypoints
        private IEnumerator ChangeWaypoints()
        {
            yield return new WaitForSeconds(Random.Range(secondsForRandomize.Min, secondsForRandomize.Max));
            Debug.Log("Cambio de wayPoints");
            var wayPointsTakenTemp = new Dictionary<Transform, FlyingEnemy>(wayPointsTaken);
            
            foreach (var wayPoint in wayPointsTakenTemp)
            {
                var wayPointNextIndex = wayPoints.Set.IndexOf(wayPoint.Key);
                if (++wayPointNextIndex >= wayPoints.Set.Count)
                {
                    wayPointNextIndex = 0;
                }
                
                var nextWayPoint = wayPoints.Set[wayPointNextIndex];
                wayPointsTaken[nextWayPoint] = wayPointsTakenTemp[wayPoint.Key];
                wayPointsTaken[nextWayPoint].currentWaypoint = nextWayPoint;
            }
            
            StartCoroutine(ChangeWaypoints());
        }
            
        //Cuando se destruye un enemigo volador, quita ese enemigo de los waypoints tomados
        private void OnFlyingEnemyDestroyed(Transform enemyTransform)
        {
            if (!wayPointsTaken.ContainsKey(enemyTransform)) return;
            wayPointsTaken.Remove(enemyTransform);
            _enemyCount--;
        }

        private void OnEnable()
        {
            FlyingEnemy.OnFlyingEnemyDead += OnFlyingEnemyDestroyed;
        }
        
        private void OnDisable()
        {
            FlyingEnemy.OnFlyingEnemyDead -= OnFlyingEnemyDestroyed;
        }
    }
}
