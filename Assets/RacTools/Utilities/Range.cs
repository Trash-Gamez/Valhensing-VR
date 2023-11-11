using Sirenix.OdinInspector;
using UnityEngine;

namespace _VanHelsingVR.Utilities
{
    [System.Serializable]
    public struct FloatRangeReference
    {
        [SerializeField] private bool useRange;

        [SerializeField, HideIf(nameof(useRange))]
        private float constant;
        
        [SerializeField, ShowIf(nameof(useRange))]
        private Range range;
        
        public float Value
        {
            get
            {
                var value = constant;
                if (useRange)
                    value = UnityEngine.Random.Range(range.Min, range.Max);
                return value;
            }
        }
    }   
    
    [System.Serializable]
    public struct Range
    {
        public static readonly Range OneToZero = new Range(1f, 0f);
        public static readonly Range ZeroToOne = new Range(0f, 1f);
    
    
        #if UNITY_EDITOR
        [VerticalGroup("Vars")]
        #endif
    
        [field: SerializeField]
        public float Min { get; private set; }
    
        #if UNITY_EDITOR
        [VerticalGroup("Vars")]
        #endif
    
        [field: SerializeField]
        public float Max { get; private set; }

        public Range(float max, float min)
        {
            Min = min;
            Max = max;
        }
    }

    [System.Serializable]
    public struct IntRange
    {
        public static readonly IntRange OneToZero = new IntRange(1, 0);
        public static readonly IntRange ZeroToOne = new IntRange(0, 1);
    
    
        [field: SerializeField]
        #if UNITY_EDITOR
        [VerticalGroup("Vars")]
        #endif 
        public int Min { get; private set; }
    
        [field: SerializeField]
        #if UNITY_EDITOR
        [VerticalGroup("Vars")]
        #endif
        public int Max { get; private set; }

        public IntRange(int max, int min)
        {
            Min = min;
            Max = max;
        }
    }
}