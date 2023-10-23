using System.Collections.Generic;
using UnityEngine;

namespace _VanHelsingVR.Events.GameEvent
{
    [CreateAssetMenu(order = 1, fileName = "GameEvent", menuName = "Events/Game Event")]
    public sealed class GameEvent : ScriptableObject 
    
    {
        private List<GameEventListener> _listeners = new();

        public void AddListener(GameEventListener listener)
        {
            if (_listeners.Contains(listener)) return;
            _listeners.Add(listener);
        }

        public void RemoveListener(GameEventListener listener)
        {
            if (!_listeners.Contains(listener)) return;
            _listeners.Remove(listener);
        }

        public void Raise()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].Raise();
            }
        }
        
    }
}
