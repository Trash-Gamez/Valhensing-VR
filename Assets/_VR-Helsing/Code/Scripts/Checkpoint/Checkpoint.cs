using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    public event System.Action OnCheckpointArrived;
    public event System.Action OnCheckpointLeft;
    
    [SerializeField] private Transform returnPoint;
    public Transform ReturnPoint => returnPoint;

    public void EnterCheckpoint()
    {
        
    }

    public void ExitCheckpoint()
    {
        
    }
}
