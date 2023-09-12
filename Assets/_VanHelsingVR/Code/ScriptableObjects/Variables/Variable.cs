using System;
using UniRx;
using UnityEngine;

namespace _VanHelsingVR.Variables
{
    public abstract class Variable<T> : ScriptableObject
    {
        private Subject<T> _subject = new();
    
        public IObservable<T> OnValueChanged => _subject;
    
        private T _value = default;
    
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
