using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _listeners = new();

    public void AddListener(GameEventListener listener)
    {
        if (_listeners.Contains(listener)) return;
        
    }

    public void RemoveListener(GameEventListener listener)
    {
        if (!_listeners.Contains(listener)) return;
        
    }
}
