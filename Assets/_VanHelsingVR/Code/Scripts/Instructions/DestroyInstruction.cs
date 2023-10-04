using UnityEngine;

namespace _VanHelsingVR.Instructions
{
    public class DestroyInstruction : MonoBehaviour
    {
        public void DestroySelf(float timeToDestroy)
        {
            Destroy(gameObject, timeToDestroy);
        }

        public void DestroyGameObject(GameObject gameObjectToDestroy)
        {
            Destroy(gameObjectToDestroy);
        }
    }
}