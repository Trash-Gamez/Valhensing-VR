using _VanHelsingVR;
using Mono.CSharp;
using UnityEngine;

public class ActivePlatform : MonoBehaviour
{
    [SerializeField] private GameObject cageWalls;
    [SerializeField] private PlatformController platformController;

    private bool _triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;
        if (other.CompareTag("Player"))
        {
            _triggered = true;
            cageWalls.SetActive(true);
            platformController.StartNextPoint(3);
            gameObject.SetActive(false);
        }
    }
}
