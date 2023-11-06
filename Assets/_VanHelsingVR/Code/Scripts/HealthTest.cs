using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTest : MonoBehaviour
{
    public RoomSpawner element;
    public int health;


    private void Update()
    {
        DestroySelf(1);
    }
    public void DestroySelf(float timeToDestroy)
    {
        if (health <= 0) {
            element.enemy.Remove(this.gameObject);
            Destroy(gameObject, timeToDestroy);
        }
        
    }
}
