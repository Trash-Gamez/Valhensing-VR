using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform returnPoint;

    public Transform ReturnPoint => returnPoint;
    
    
}
