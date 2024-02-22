using UnityEngine;

namespace _VanHelsingVR.Singletons
{
    public class RegulatorMonoSingleton<T> : MonoBehaviour where T : Component
    {
        protected static T instance = null;
        public static bool HasInstance => instance;

        public float InitializationTime { get; private set; }
    
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
                        go.hideFlags = HideFlags.HideAndDontSave;
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
            InitializationTime = Time.time;
            DontDestroyOnLoad(gameObject);

            T[] oldInstances = FindObjectsOfType<T>();
            for (int i = 0; i < oldInstances.Length; i++)
            {
                if (oldInstances[i].GetComponent<RegulatorMonoSingleton<T>>().InitializationTime < InitializationTime) 
                {
                    Destroy(oldInstances[i].gameObject);
                }
            }
        
            instance = this as T;
        }
    }
}