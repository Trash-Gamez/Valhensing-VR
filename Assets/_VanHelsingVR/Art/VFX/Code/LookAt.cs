using UnityEngine;

namespace _VanHelsingVR.Art.VFX.Code
{
    public class LookAt : MonoBehaviour
    {
        [SerializeField] private Transform target;
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        }

   
        void Update()
        {
            transform.LookAt(target);
        }
    }
}
