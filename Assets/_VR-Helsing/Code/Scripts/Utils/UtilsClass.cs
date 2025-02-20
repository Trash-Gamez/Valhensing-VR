using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UtilsClass
{
    #region Collections Utils
    public static T PickRandom<T>(this List<T> list)
    {
        if(list.IsNullOrEmpty()) return default;
        return list[Random.Range(0, list.Count - 1)];
    }

    public static bool IsNullOrEmpty<T>(this List<T> list)
    {
        if (list == null) return false;
        
        return list.Count < 0;
    }
    public static bool IsNullOrEmpty<T>(this T[] list)
        {
            return list == null || list.Length == 0;
        }
            
    public static T PickRandom<T>(this List<T> list, System.Func<T, bool> condition)
    {
        List<T> filteredList = list.Where(condition).ToList();
        if (filteredList.Count == 0) return default;
        return filteredList.PickRandom();
    }


    public static T PickRandom<T>(this List<T> list, int seed)
    {
        if (seed != -1)
            UnityEngine.Random.InitState(seed);

        return list[UnityEngine.Random.Range(0, list.Count)];
    }        
    #endregion
}
