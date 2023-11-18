using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateInstruction : MonoBehaviour
{
    [SerializeField] private Transform pointToSpawn;
    public void Instantiate(GameObject gameObject)
    {
        var pos = pointToSpawn != null ? pointToSpawn.position : transform.position; 
        Instantiate(gameObject, pos, Quaternion.identity);
    }
}
