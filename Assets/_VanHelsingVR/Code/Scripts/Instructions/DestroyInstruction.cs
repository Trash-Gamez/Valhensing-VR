using System.Collections;
using UnityEngine;

namespace _VanHelsingVR.Instructions
{
    [DefaultExecutionOrder(2)]
    public class DestroyInstruction : MonoBehaviour
    {
        public void DestroyObject(GameObject objectToDestroy)
        {
            Destroy(objectToDestroy);
        }
        
        public void DestroySelf(float timeToDestroy)
        {
            StartCoroutine(DestroySelfCor(timeToDestroy));
        }

        private IEnumerator DestroySelfCor(float timeToDestroy)
        {
            yield return null;
            Destroy(gameObject, timeToDestroy);
        }
    }
}