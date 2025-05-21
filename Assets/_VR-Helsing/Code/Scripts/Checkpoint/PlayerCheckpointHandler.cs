using System;
using System.Collections.Generic;
using Autohand;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;


/// <summary>
/// Clase encargada de manejar los checkpoints
/// </summary>
public class PlayerCheckpointHandler : MonoBehaviour
{
    [SerializeField] private AutoHandPlayer player;
    
    [Required]
    [SerializeField] private Checkpoint defaultCheckpoint;

    [SerializeField] private DeadzoneTunnelingProvider provider;
    [SerializeField] private float waitToTeleport;
    
    private Checkpoint _currentCheckpoint;

    private void Awake()
    {
        _currentCheckpoint = defaultCheckpoint;
    }

    public void UseLastCheckPoint()
    {
        Timing.RunCoroutine(UseCheckpoint(_currentCheckpoint));
    }

    private IEnumerator<float> UseCheckpoint(Checkpoint checkpoint)
    {
        player.useMovement = false;

        provider.SetIsInDeadZone(true);
        
        yield return Timing.WaitForSeconds(waitToTeleport);
        
        player.SetPosition(checkpoint.ReturnPoint.position);
        player.SetRotation(checkpoint.ReturnPoint.localRotation);
        
        //Esperar a colocar todo en escena
        //Quitar vida
        yield return Timing.WaitForSeconds(0.25f);
        
        provider.SetIsInDeadZone(false);
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
