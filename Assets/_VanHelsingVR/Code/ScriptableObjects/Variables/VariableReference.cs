using System;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

namespace _VanHelsingVR.Variables
{
    [Serializable]
    public abstract class VariableReference<T>
    {
        [SerializeField] private bool useConstant;

        [SerializeField]
    #if UNITY_EDITOR
        [HideIf(nameof(useConstant))]
    #endif
        private Variable<T> reference;
        
        [SerializeField]
    #if UNITY_EDITOR
        [ShowIf(nameof(useConstant))]
    #endif
        private T constant;

        public T value
        {
            get
            {
                return useConstant ? constant : reference.Value;
            }
        }
    }
}
