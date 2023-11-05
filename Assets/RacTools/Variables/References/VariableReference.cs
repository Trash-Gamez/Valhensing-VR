using System;
using UnityEngine;

namespace RacTools.Variables
{
    public abstract class VariableReference
    {
        public enum VariableType
        {
            Constant,
            Reference,
            Instance
        }
        
        [SerializeField] protected VariableType variableType;
    }
    
    [Serializable]
    public class VariableReference<T> : VariableReference
    {
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
                    VariableType.Instance => GetInstance().Value,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        private Variable<T> GetInstance()
        {
            Debug.Log("Se llamo la instancia");
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
