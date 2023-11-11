using UnityEngine;

namespace _VanHelsingVR.Proyectile
{
    public class SimpleProyectile : MonoBehaviour
    {
        [HideInInspector]
        public float speed;
        [HideInInspector]
        public Transform target;
        
        
    
        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.LookAt(target);
        }
    }
}
