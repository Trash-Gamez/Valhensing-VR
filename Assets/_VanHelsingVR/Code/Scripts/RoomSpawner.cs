using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private List <GameObject> enemy;   
    [SerializeField] private Animator[] DoorAnim;
    private bool _onCombat;
    private BoxCollider[] MyColliders;

    private void Awake()
    {
        MyColliders = GetComponents<BoxCollider>();   
    }
    void Start()
    {
        foreach(GameObject prefab in enemy)
        {
                prefab.SetActive(false);
        }      
    }


    public void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")){

            foreach(Animator doors in DoorAnim)
            {
                doors.Play("Close");               
            }
            foreach(GameObject prefab in enemy)
            {
                prefab.SetActive(true);
            }
            foreach(BoxCollider collider in MyColliders)
            {
                collider.enabled = false;
            }
            AudioManager.Instance.SwampMusic("GameplayMusic02");
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
        foreach (Animator doors in DoorAnim)
        {
            doors.Play("Open");
        }

        AudioManager.Instance.SwampMusic("GameplayMusic03");
    }

    public void DeleteEnemy(GameObject temporalEnemy)
    {
        enemy.Remove(temporalEnemy);
    }
}
