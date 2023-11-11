using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Object = UnityEngine.Object;

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
        
        private bool _isInstance = false;

        private Variable<T> _instance
        {
            get
            {
                if (variableType != VariableType.Instance) return null;
                
                if (reference == null)
                    throw new NullReferenceException("There is no reference value to instantiate variable");
                if (!_isInstance)
                {
                    reference = Object.Instantiate(reference);
                    _isInstance = true;
                }

                return reference;
            }
        }

        public T Value
        {
            get
            {
                return variableType switch
                {
                    VariableType.Constant => constant,
                    VariableType.Reference => reference.Value,
                    VariableType.Instance => _instance.Value,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            set
            {
                switch (variableType)
                {
                    case VariableType.Constant:
                        constant = value;
                        break;
                    case VariableType.Reference:
                        reference.Value = value;
                        break;
                    case VariableType.Instance:
                        _instance.Value = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private string _objectName = null;
        private string ObjectName
        {
            get
            {
                if (string.IsNullOrEmpty(_objectName))
                {
                    _objectName = variableType switch
                    {
                        VariableType.Constant => "*constant variable*",
                        VariableType.Reference => reference.name,
                        VariableType.Instance => _instance.name,
                        _ => throw new ArgumentOutOfRangeException()    
                    };
                }

                return _objectName;
            }
        }
    }
    
    //TODO: hacer un setter pero en las variable, no en la referencia
    /*
    public class ContainsSetterException : Exception
    {
        public ContainsSetterException(string objectName, IVariableSetter setter) 
            : base($"There is already a Setter in {objectName} is {setter.BaseObjectSetter.name}") {}
    }

    public class DifferentSetterException : Exception
    {
        public DifferentSetterException(string message) : base(message){}
    }

    public class NullSetterException : Exception
    {
        public NullSetterException(string message) : base(message){}
    }
    */
}
