using UnityEngine;

namespace _VanHelsingVR.Health
{
    [RequireComponent(typeof(Collider))]
    [DisallowMultipleComponent]
    public abstract class BaseHitBox : MonoBehaviour
    {
        public event System.Action<OnHitBoxHitArgs> OnHitBoxHit = delegate {};
        
        [field:SerializeField]
        public TeamEnum TeamHitBox { get; protected set; }
        
        [field:SerializeField]
        public Collider Collider { get; protected set; }

        protected OnHitBoxHitArgs onHitBoxHitArgs;
        
        protected virtual void Awake()
        {
            if(!Collider) Collider = GetComponent<Collider>();
            
            onHitBoxHitArgs = new OnHitBoxHitArgs()
            {
                HitBox = this
            };
        }
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            //condición
            _OnHit();
        }

        private void _OnHit()
        {
            
            OnHit();
        }

        public void Enable()
        {
            enabled = true;
            Collider.enabled = true;
            OnHitBoxEnabled();
        }
        
        public void Disable()
        {
            enabled = false;
            Collider.enabled = false;
            OnHitBoxDisabled();
        }

        protected virtual void OnHit(){}
        protected virtual void OnHitBoxDisabled() {}
        protected virtual void OnHitBoxEnabled() {}
        
        
    }
}