using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Deadzone : MonoBehaviour
{
    public static event Action OnDeadzoneTouched; 
    
    protected Collider collider;

    protected virtual void Start()
    {
        collider = GetComponent<Collider>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        OnDeadzoneTouched?.Invoke();
    }
}
