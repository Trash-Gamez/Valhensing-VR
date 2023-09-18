using System;
using UniRx;
using UnityEngine;

namespace _VanHelsingVR.Events
{
    #if UNITY_EDITOR
    public interface IEditorReactiveEvent
    {
        void RaiseDefaultEvent();
    }
    #endif
    
    public abstract class ReactiveEvent<T> : ScriptableObject
        
    #if UNITY_EDITOR
    ,IEditorReactiveEvent
    #endif
    
    {
        protected Subject<T> subject;

        public virtual IObservable<T> Event => subject;

        public void Raise(T param)
        {
            subject.OnNext(param);
        }

        #if UNITY_EDITOR
        public void RaiseDefaultEvent()
        {
            Raise(default);
        }
        #endif
    }
    
    [CreateAssetMenu(order = 0,fileName = "Reactive Event", menuName = "Events/Reactive/Reactive Event")]
    public sealed class ReactiveEvent : ReactiveEvent<Unit>{}
}
