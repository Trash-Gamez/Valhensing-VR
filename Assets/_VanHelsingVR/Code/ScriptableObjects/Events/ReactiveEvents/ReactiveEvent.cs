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
    , IEditorReactiveEvent
#endif

    {
        protected Subject<T> subject;

        public virtual IObservable<T> Event => subject;

        public void Raise(T param)
        {
            subject.OnNext(param);
        }

        private void OnEnable()
        {
            subject = new Subject<T>();
        }

#if UNITY_EDITOR
        public void RaiseDefaultEvent()
        {
            Raise(default);
        }
#endif
    }

    public abstract class ReactiveEvent<T1, T2> : ScriptableObject
    {
        protected Subject<(T1,T2)> subject;

        public virtual IObservable<(T1, T2)> Event => subject;

        public void Raise(T1 param1, T2 param2)
        {
            subject.OnNext((param1, param2));
        }

        private void OnEnable()
        {
            subject = new Subject<(T1, T2)>();
        }
    }
    
    [CreateAssetMenu(order = 0,fileName = "Reactive Event", menuName = "Events/Reactive/Reactive Event")]
    public sealed class ReactiveEvent : ReactiveEvent<Unit>{}
}
