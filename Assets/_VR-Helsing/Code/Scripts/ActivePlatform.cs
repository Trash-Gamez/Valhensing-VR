using _VanHelsingVR;
using Mono.CSharp;
using UnityEngine;

public class ActivePlatform : MonoBehaviour
{
    [SerializeField] private GameObject cageWalls;
    [SerializeField] private PlatformController platformController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cageWalls.SetActive(true);
            platformController.StartNextPoint(3);
            gameObject.SetActive(false);
        }
    }
}
