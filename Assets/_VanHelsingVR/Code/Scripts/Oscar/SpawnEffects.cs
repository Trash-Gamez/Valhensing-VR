using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffects : MonoBehaviour
{
    [SerializeField] private GameObject effect01;
    [SerializeField] private GameObject effect02;
    public void Effect01()
    {
        GameObject actualObject = Instantiate(effect01, transform.position, Quaternion.identity);    
    }

    public void Effect02()
    {
        GameObject actualObject = Instantiate(effect02, transform.position, Quaternion.identity);
    }
}
