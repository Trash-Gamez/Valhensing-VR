using UnityEngine;

namespace RacTools.Utils
{
    public class OptimizedMap
    {
        private readonly float _factor;
        private readonly float _inputMin;
        private readonly float _outputMin, _outputMax;

        public static OptimizedMap Create(float inputMin, float inputMax, float outputMin, float outputMax)
        {
            var optMap = new OptimizedMap(inputMin, inputMax, outputMin, outputMax);
            return optMap;
        }

        public static OptimizedMap Create(Range inputRange, Range outputRange)
        {
            var optMap = new OptimizedMap(inputRange.Min, inputRange.Max, outputRange.Min, outputRange.Max);
            return optMap;
        }

        private OptimizedMap(float inputMin, float inputMax, float outputMin, float outputMax)
        {
            _inputMin = inputMin;
            _outputMin = outputMin;
            _outputMax = outputMax;
            _factor = (outputMax - outputMin) / (inputMax - inputMin);
        }

        public float Map(float value)
        {
            return (value - _inputMin) * _factor + _outputMin;
        }
    
        public float MapClamp(float value)
        {
            var result = Map(value);
            return Mathf.Clamp(result, _outputMin, _outputMax);
        }
    }
}
