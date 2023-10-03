using System;
using UnityEngine;

namespace _VanHelsingVR.Events
{
    [Serializable]
    public abstract class ReactiveEventReference<T>
    {
        [SerializeField] private ReactiveEvent<T> reactiveEvent;

        public virtual IObservable<T> Event
        {
            get => reactiveEvent.Event;
        }
    }
}
