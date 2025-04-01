using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace _VanHelsingVR.Events
{
    public class IntReactiveEventListener : MonoBehaviour
    {
        [SerializeField] private ReactiveEventReference<int> eventReference;
        [SerializeField] private UnityEvent<int> UnityEvent;

        private IDisposable susbcription;
        
        private void Raise(int value)
        {
            UnityEvent?.Invoke(value);
        }
        
        private void OnEnable()
        {
            susbcription = eventReference.Event.Subscribe(Raise);
        }

        private void OnDisable()
        {
            susbcription.Dispose();
        }
    }
}
