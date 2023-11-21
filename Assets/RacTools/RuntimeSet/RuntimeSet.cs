using System.Collections.Generic;
using UnityEngine;

namespace RacTools.RuntimeSet
{
    public class RuntimeSet<T> : ScriptableObject where T : Object
    {
        [field: SerializeField]
        public List<T> Set { get; private set; } = new List<T>();

        public void AddToSet(T objectToAdd)
        {
            if (Set.Contains(objectToAdd)) return;
            Set.Add(objectToAdd);
        }

        public void RemoveFromSet(T objectToRemove)
        {
            if (!Set.Contains(objectToRemove)) return;
            Set.Remove(objectToRemove);
        }
    }
}