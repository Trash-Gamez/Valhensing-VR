using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform returnPoint;

    public Transform ReturnPoint => returnPoint;
    
    
}
