using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Deadzone : MonoBehaviour
{
    public static event Action OnDeadzoneTouched; 
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        OnDeadzoneTouched?.Invoke();
    }
}
