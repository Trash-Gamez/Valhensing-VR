using RacTools.Utils;

namespace _VanHelsingVR.Utilities
{
    public static class UtilitieExtensions
    {
        /// <summary>
        /// This Function return a mapped value between two ranges of value
        /// </summary>
        /// <param name="in">The In Value that will be evaluate</param>
        /// <param name="inMin">The Min value used to Map the in param</param>
        /// <param name="inMax">The Max value used to Map the in param</param>
        /// <param name="outMin">The Min value used to Map the out value</param>
        /// <param name="outMax">The Max value used to Map the out value</param>
        /// <returns>Returns the mapped value from the in param</returns>
        public static float Map(float inValue, float inMin, float inMax, float outMin, float outMax)
        {
            return (inValue - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }
    
        /// <summary>
        /// This Function return a mapped value between two ranges of value
        /// </summary>
        /// <param name="inValue">The In Value that will be evaluate</param>
        /// <param name="inRange">The Range of values used to Map de in param</param>
        /// <param name="outRange">The Range of values used to Map de out value</param>
        /// <returns>Returns the mapped value from the in param</returns>
        public static float Map(float inValue, Range inRange, Range outRange)
        {
            return Map(inValue, inRange.Min, inRange.Max, outRange.Min, outRange.Max);
        }
    }
}
