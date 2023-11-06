using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Collider[] door;
    public List <GameObject> enemy;
    
    public Animator[] shortDoor;
    public int enemies;
    
    void Start()
    {
        foreach(Collider doors in door){
                doors.isTrigger = true;
            }
        foreach(GameObject prefab in enemy){
                prefab.SetActive(false);
            }
    }

    
    void Update()
    {
        enemies = enemy.Count;

        if(enemies <= 0){
            foreach(Animator doors in shortDoor){
                doors.Play("Open");
            }
            foreach(Collider doors in door){
                doors.isTrigger = true;
            }
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")){
            foreach(Animator doors in shortDoor){
                doors.Play("Close");
            }
            
            foreach(Collider doors in door){
                doors.isTrigger = false;
            }
            foreach(GameObject prefab in enemy){
                prefab.SetActive(true);
            }

        }
    }
}
