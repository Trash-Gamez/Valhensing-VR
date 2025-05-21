using System;
using Autohand;
using Sirenix.OdinInspector;
using UnityEngine;


/// <summary>
/// Clase encargada de manejar los checkpoints
/// </summary>
public class PlayerCheckpointHandler : MonoBehaviour
{
    [Required]
    [SerializeField]
    private Checkpoint defaultCheckpoint;

    [SerializeField] private AutoHandPlayer player;
    
    private Checkpoint _currentCheckpoint;

    private void Awake()
    {
        _currentCheckpoint = defaultCheckpoint;
    }

    public void UseLastCheckPoint()
    {
        
    }

    private void HandleTriggerCheckpoint(Checkpoint enteringCheckpoint)
    {
        if (enteringCheckpoint && _currentCheckpoint == enteringCheckpoint) return;

        _currentCheckpoint = enteringCheckpoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Checkpoint")) return;
        if(!other.TryGetComponent(out Checkpoint check)) return;
        
        HandleTriggerCheckpoint(check);
    }
}
