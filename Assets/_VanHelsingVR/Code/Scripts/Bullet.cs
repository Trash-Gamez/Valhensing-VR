using UnityEngine;

namespace _VanHelsingVR
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed;
        public Vector3? direction;        

        private void Start()
        {
            Destroy(gameObject, 10);
        }
        void Update()
        {
            if (direction == null) 
            {
                gameObject.transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
            }
            else
            {
                gameObject.transform.position += direction.Value * speed * Time.deltaTime;
            }
           
        }
    }
}
