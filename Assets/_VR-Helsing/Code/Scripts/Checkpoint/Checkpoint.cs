using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    private BoxCollider _boxCollider;

    private void Start()
    {
        if(!_boxCollider) _boxCollider = GetComponent<BoxCollider>();

        _boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
    }
}
