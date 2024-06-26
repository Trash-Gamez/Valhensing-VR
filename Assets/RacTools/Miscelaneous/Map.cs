using RacTools.Utilities;
using UnityEngine;

namespace RacTools.Miscelaneous
{
    public static class Map
    {
        public static float MapFloat(float value, float inMin, float inMax, float outMin, float outMax)
        {
            return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        public static float MapFloatRange(float value, Range inRange, Range outRange, bool clampValue = false)
        {
            var result = MapFloat(value, inRange.Min, inRange.Max, outRange.Min, outRange.Max);
            return clampValue ? Mathf.Clamp(result, outRange.Min, outRange.Max) : result;
        }
    }
}