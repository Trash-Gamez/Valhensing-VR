using System;
using UnityEngine;

namespace RacTools.Variables
{
    [Serializable]
    public class VariableReference<T>
    {
        public enum VariableType
        {
            Constant,
            Reference,
            Instance
        }
        
        [SerializeField] private VariableType variableType;
        
        [SerializeField]
        private Variable<T> reference = null;
        
        [SerializeField]
        private T constant;

        private Variable<T> _instance = null;
        
        
        public T Value
        {
            get
            {
                return variableType switch
                {
                    VariableType.Constant => constant,
                    VariableType.Reference => reference.Value,
                    VariableType.Instance => GetInstance().Value
                };
            }
        }

        private Variable<T> GetInstance()
        {
            if (reference == null)
            {
                Debug.LogError("There is no reference value to instantiate variable");
                return default;
            }

            if (_instance == null)
                _instance = UnityEngine.Object.Instantiate(reference);

            return _instance;
        }
    }
}
