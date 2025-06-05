using System;
using UnityEngine;

public class DeadzoneCheckpoint : Deadzone
{
    [SerializeField] private Checkpoint activeCheckpoint;

    protected override void Start()
    {
        base.Start();
        collider.enabled = false;
    }

    private void SetCheckpointIsActive(bool isActive)
    {
        collider.enabled = isActive;
    }

    private void OnCheckpointLeft()
    {
        SetCheckpointIsActive(false);
    }

    private void OnCheckpointArrived()
    {
        SetCheckpointIsActive(true);
    }
    
    private void OnEnable()
    {
        activeCheckpoint.OnCheckpointArrived += OnCheckpointArrived;
        activeCheckpoint.OnCheckpointLeft += OnCheckpointLeft;
    }

    private void OnDisable()
    {
        activeCheckpoint.OnCheckpointArrived -= OnCheckpointArrived;
        activeCheckpoint.OnCheckpointLeft -= OnCheckpointLeft;
    }
}
