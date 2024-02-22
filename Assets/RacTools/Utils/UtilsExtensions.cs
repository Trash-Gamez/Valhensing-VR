using System.Collections.Generic;
using UnityEngine;

namespace RacTools.Utils
{
    public static class UtilsExtensions
    { 
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count <= 0;
        }

        public static T GetOrAdd<T>(this GameObject gameObject, bool warningIfAddingComponent = false) where T : MonoBehaviour
        {
            if (gameObject.TryGetComponent<T>(out var component)) return component;
        
            if(warningIfAddingComponent) 
                Debug.LogWarning($"{gameObject.name} does not contains component of type '{typeof(T).Name}'", gameObject);
            
            component = gameObject.AddComponent<T>();
            return component;
        }

        public static void Log(this string message) => Debug.Log(message);
        public static void LogWarning(this string message) => Debug.LogWarning(message);
        public static void LogError(this string message) => Debug.LogError(message);
    }
}
