using Autohand;
using UnityEngine;

namespace _VR_Helsing.Gun
{
    [RequireComponent(typeof(SphereCollider))]
    public class GunPointerUI : MonoBehaviour
    {
        [SerializeField] private Hand hand;
        [SerializeField] private float radius = 1;
    
        [SerializeField] private SphereCollider pointerCollider;

        public Hand AttachedHand => hand;
    
        private void Start()
        {
            pointerCollider.radius = radius;
        }
    
    
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!pointerCollider)
                pointerCollider = GetComponent<SphereCollider>();
            pointerCollider.radius = radius;
        }
#endif
    }
}
