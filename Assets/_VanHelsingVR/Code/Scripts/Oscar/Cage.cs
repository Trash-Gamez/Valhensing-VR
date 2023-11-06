using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField] GameObject invisibleWall;
    [SerializeField] GameObject invisibleCage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            invisibleWall.SetActive(true);
            invisibleCage.SetActive(false);
        }
    }
}
