using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;

    [SerializeField] private UnityEvent callback;

    private void OnEnable()
    {
        
    }
}
