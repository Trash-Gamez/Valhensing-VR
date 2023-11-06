using System;
using UnityEngine;

namespace _VanHelsingVR.Health
{
    //Un componente que te ayuda a popular las hitboxes ya existentes
    
    [RequireComponent(typeof(Hitbox))]
    public class HitboxDataPopulator : MonoBehaviour
    {
        [SerializeField] private Hitbox hitboxToPopulate;
        [SerializeField] private HitBoxData hitboxData;
        [SerializeField] private bool destroyAfterPopulate = true;
        
        private void Awake()
        {
            hitboxToPopulate ??= GetComponent<Hitbox>();
        }

        private void Start()
        {
            PupulateHitbox();
        }

        private void PupulateHitbox()
        {
            hitboxToPopulate.PopulateData(hitboxData);
            
            if(destroyAfterPopulate)
                Destroy(this);
        }
    }
}