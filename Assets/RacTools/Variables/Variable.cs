using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace RacTools.Variables
{
    public abstract class Variable<T> : ScriptableObject
    {
        private Subject<T> _subject = new();
    
        public IObservable<T> OnValueChanged => _subject;
        
        [SerializeField, HideInInspector]
        private T _value;
        
        [ShowInInspector]
        public virtual T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _subject.OnNext(_value);
            }
        }
    }
}
