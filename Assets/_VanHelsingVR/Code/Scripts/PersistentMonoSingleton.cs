using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentMonoSingleton<T> : MonoBehaviour where T : Component
{
    protected static T instance = null;
    public static bool HasInstance => instance;
    
    public bool AutoUnParentOnAwake = true;
    
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
        
        if (AutoUnParentOnAwake)
        {
            transform.SetParent(null);
        }
        
        if (!instance)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
            return;
        }

        if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
