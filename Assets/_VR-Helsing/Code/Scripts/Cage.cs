using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField] private GameObject cageWalls;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cageWalls.SetActive(false);
        }
    }
}
