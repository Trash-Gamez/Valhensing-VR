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
    private bool _usingCheckpoint = false;
    
    private void Awake()
    {
        _currentCheckpoint = defaultCheckpoint;
    }

    public void UseLastCheckPoint()
    {
        if (_usingCheckpoint) return;
        Timing.RunCoroutine(UseCheckpoint(_currentCheckpoint));
    }

    private IEnumerator<float> UseCheckpoint(Checkpoint checkpoint)
    {
        _usingCheckpoint = true;

        provider.SetIsInDeadZone(true);
        
        yield return Timing.WaitForSeconds(waitToTeleport);
        
        player.useMovement = false;
        
        player.SetPosition(checkpoint.ReturnPoint.position);
        player.SetRotation(checkpoint.ReturnPoint.localRotation);
        
        //Esperar a colocar to do en escena
        //TODO: Quitar vida
        yield return Timing.WaitForSeconds(0.25f);
        
        player.useMovement = true;
        provider.SetIsInDeadZone(false);
        
        _usingCheckpoint = false;
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

    private void OnEnable() => Deadzone.OnDeadzoneTouched += UseLastCheckPoint;

    private void OnDisable() => Deadzone.OnDeadzoneTouched -= UseLastCheckPoint;
}
