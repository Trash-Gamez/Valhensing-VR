using System;
using _VanHelsingVR.Enemy;
using _VR_Helsing.HealthSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class DummySpawner : MonoBehaviour
{
    [SerializeField] private float spawnSecondsAfterDead;
    [SerializeField] private CowardEnemyStateMachine enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CowardEnemyStateMachine currentDummy;

    private Quaternion _cacheRotation;

    private void Start()
    {
        currentDummy.GetComponent<HealthSystem>().onDead.AddListener(OnDummyDead);
    }

    public void OnDummyDead()
    {
        _cacheRotation = currentDummy.transform.rotation;
        currentDummy.GetComponent<HealthSystem>().onDead.RemoveListener(OnDummyDead);
        Invoke(nameof(SpawnDummy), spawnSecondsAfterDead);
    }

    private void SpawnDummy()
    {
        currentDummy = Instantiate(enemyPrefab, spawnPoint.position, _cacheRotation);
        currentDummy.GetComponent<HealthSystem>().onDead.AddListener(OnDummyDead);
    }
}
