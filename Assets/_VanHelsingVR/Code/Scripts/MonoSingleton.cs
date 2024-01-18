using System;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    protected static T instance = null;
    public static bool HasInstance => instance;
    
    public static T Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<T>();
                if (!instance)
                {
                    var go = new GameObject("New_" + typeof(T).Name);
                    instance = go.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        InitializeSingleton();
    }

    protected virtual void InitializeSingleton()
    {
        if (!Application.isPlaying) return;
        instance = this as T;
    }
}
