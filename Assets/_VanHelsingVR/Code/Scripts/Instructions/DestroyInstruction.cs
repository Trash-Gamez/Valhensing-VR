using UnityEngine;

namespace _VanHelsingVR.Instructions
{
    [DefaultExecutionOrder(1)]
    public class DestroyInstruction : MonoBehaviour
    {
        public RoomSpawner element;
        public void DestroySelf(float timeToDestroy)
        {
            element.enemy.Remove(this.gameObject);
            Destroy(gameObject, timeToDestroy);
        }

        public void DestroyGameObject(GameObject gameObjectToDestroy)
        {
            Destroy(gameObjectToDestroy);
        }
    }
}