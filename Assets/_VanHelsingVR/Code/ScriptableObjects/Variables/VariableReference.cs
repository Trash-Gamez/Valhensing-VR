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
        [SerializeField] private bool useConstant = true;

        #if UNITY_EDITOR
        [HideIf(nameof(useConstant))]
        #endif
        [SerializeField]
        private Variable<T> reference;
        
        #if UNITY_EDITOR
        [ShowIf(nameof(useConstant))]
        #endif
        [SerializeField]
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
