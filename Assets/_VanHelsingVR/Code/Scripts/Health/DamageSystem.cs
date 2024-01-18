using System.Collections.Generic;
using UnityEngine;

namespace _VanHelsingVR.Health
{
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class DamageSystem : MonoBehaviour
    {
        [SerializeField] private HashSet<HitBox> childrenHitBoxes;

        private void Start()
        {
            GetChildrenHitBoxes();
            
            //Desactiva todas las hitboxes hijas
            foreach (var hitbox in childrenHitBoxes)
            {
                hitbox.Disable();
            }
        }
        
        [ContextMenu("Get Children HotBoxes")]
        private void GetChildrenHitBoxes()
        {
            HitBox[] newChildrenHitBox = GetComponentsInChildren<HitBox>();
            
            foreach (var childHitBox in newChildrenHitBox)
            {
                if(childHitBox.transform.parent != transform) continue;
                childrenHitBoxes.Add(childHitBox);
            }
        }
    }
}
