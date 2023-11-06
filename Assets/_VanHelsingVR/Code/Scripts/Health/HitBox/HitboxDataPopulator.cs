using System;
using System.Collections.Generic;
using UnityEngine;

namespace _VanHelsingVR.Health
{
    //Un componente que te ayuda a popular las hitboxes ya existentes
    
    [RequireComponent(typeof(Hitbox))]
    [DefaultExecutionOrder(-1)]
    public class HitboxDataPopulator : MonoBehaviour
    {
        [SerializeField] private Hitbox hitboxToPopulate;
        [SerializeField] private List<DataPopulator> dataPopulators = new List<DataPopulator>();
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
            foreach (var populator in dataPopulators)        
            {
                hitboxToPopulate.AddHitBoxData(populator.dataType, populator.attribute);
            }
            
            if(destroyAfterPopulate)
                Destroy(this);
        }

        #region Editor Function
        private void OnValidate()
        {
            hitboxToPopulate ??= GetComponent<Hitbox>();
        }
        #endregion

        [System.Serializable]
        public class DataPopulator
        {
            public HitBoxDataType dataType;
            public HitBoxDataAttribute attribute;
        }
    }
}