using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemy;
    [SerializeField] private GameObject[] Doors;
    [SerializeField] private GameObject[] Stairs;
    //[SerializeField] private GameObject[] ActiveAfterBattle;
    private bool _onCombat;
    private BoxCollider MyCollider;

    private void Awake()
    {
        MyCollider = GetComponent<BoxCollider>();
    }
    void Start()
    {
        foreach (GameObject prefab in enemy)
        {
            prefab.SetActive(false);
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            foreach (GameObject doors in Doors)
            {
                doors.SetActive(true);
            }
            foreach (GameObject stair in Stairs)
            {
                stair.SetActive(false);
            }
            /* foreach (GameObject activeObject in ActiveAfterBattle)
            {
                activeObject.SetActive(false);
            } */
            foreach (GameObject prefab in enemy)
            {
                prefab.SetActive(true);
            }

            MyCollider.enabled = false;

            //AudioManager.Instance.SwampMusic("GameplayMusic02");
            StartCoroutine(OnCombat());
        }
    }

    IEnumerator OnCombat()
    {
        _onCombat = true;
        while (_onCombat)
        {
            if (enemy.Count <= 0)
            {
                _onCombat = false;
            }
            yield return null;
        }
        OpenGates();
    }

    private void OpenGates()
    {
        foreach (GameObject doors in Doors)
        {
            doors.SetActive(false);
        }

        foreach (GameObject stair in Stairs)
        {
            stair.SetActive(true);
        }

        /* foreach (GameObject activeObject in ActiveAfterBattle)
        {
            activeObject.SetActive(true);
        } */

        //AudioManager.Instance.SwampMusic("GameplayMusic03");
    }

    public void DeleteEnemy(GameObject temporalEnemy)
    {
        enemy.Remove(temporalEnemy);
    }
}
