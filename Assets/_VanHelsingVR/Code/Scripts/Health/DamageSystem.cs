using System.Collections.Generic;
using UnityEngine;

namespace _VanHelsingVR.Health
{
    [DisallowMultipleComponent]
    public class DamageSystem : MonoBehaviour
    {
        public delegate void OnHitDelegate(HitArgs args);
        public event OnHitDelegate OnHit;
        
        [SerializeField] private List<BaseHitBox> childrenHitBoxes;

        private void Start()
        {
            GetHitBoxesInChildren();
            
            foreach (var hitbox in childrenHitBoxes)
            {
                hitbox.Disable();
            }
        }
        
        [ContextMenu("Get Children HotBoxes")]
        private void GetHitBoxesInChildren()
        {
            BaseHitBox[] newChildrenHitBox = GetComponentsInChildren<BaseHitBox>();
            
            foreach (var childHitBox in newChildrenHitBox)
            {
                if(childrenHitBoxes.Contains(childHitBox)) continue;
                childrenHitBoxes.Add(childHitBox);
            }
        }
    }
}
