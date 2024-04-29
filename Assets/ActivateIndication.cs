using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateIndication : MonoBehaviour
{
    public GameObject sign;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sign.SetActive(true);
        }
    }
}
