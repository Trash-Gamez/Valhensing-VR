using System;

namespace RacTools.Utils
{
    public class IntOptimizedMap
    {
        private readonly int _factor;
        private readonly int _inputMin;
        private readonly int _outputMin, _outputMax;

        public static IntOptimizedMap Create(int inputMin, int inputMax, int outputMin, int outputMax)
        {
            var optMap = new IntOptimizedMap(inputMin, inputMax, outputMin, outputMax);
            return optMap;
        }

        public static IntOptimizedMap Create(IntRange inputRange, IntRange outputRange)
        {
            var optMap = new IntOptimizedMap(inputRange.Min, inputRange.Max, outputRange.Min, outputRange.Max);
            return optMap;
        }

        private IntOptimizedMap(int inputMin, int inputMax, int outputMin, int outputMax)
        {
            _inputMin = inputMin;
            _outputMin = outputMin;
            _factor = (outputMax - outputMin) / (inputMax - inputMin);
        }

        public int Map(int value)
        {
            return (value - _inputMin) * _factor + _outputMin;
        }

        public int MapClamp(int value)
        {
            var result = Map(value);
            return Math.Clamp(result, _outputMin, _outputMax);
        }
    }
}
