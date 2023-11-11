using UnityEngine;

namespace _VanHelsingVR.Instructions
{
    [DefaultExecutionOrder(1)]
    public class DestroyInstruction : MonoBehaviour
    {
        [SerializeField] RoomSpawner element;

        public void DestroyObject(GameObject objectToDestroy)
        {
            Destroy(objectToDestroy);
        }
        
        public void DestroySelf(float timeToDestroy)
        {
            //?????????????
            if(element!=null) element.enemy.Remove(this.gameObject);
            Destroy(gameObject, timeToDestroy);
        }

        public void DestroyGameObject(GameObject gameObjectToDestroy)
        {
            Destroy(gameObjectToDestroy);
        }
    }
}