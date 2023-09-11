
using System;
using UnityEngine;

namespace _VanHelsingVR.Variables
{
    [Serializable]
    public abstract class VariableReference<T>
    {
        [SerializeField] private bool _useConstant;

        [SerializeField,] private Variable<T> _reference;
        [SerializeField] private T _constant;

        public T value
        {
            get
            {
                return _useConstant ? _constant : _reference.Value;
            }
        }
    }
}
