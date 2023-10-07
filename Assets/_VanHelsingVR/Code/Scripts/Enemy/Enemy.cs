using System.Collections;
using UnityEngine;
using _VanHelsingVR.Interaction;

namespace _VanHelsingVR.Enemy
{
    public class Enemy : Hittable
    {
        [SerializeField] Renderer[] renderers;

        private Coroutine _hitCoroutine = null;

        private MaterialPropertyBlock _materialRed;
        private MaterialPropertyBlock MaterialRed
        {
            get
            {
                if (_materialRed == null)
                    _materialRed = new();
                return _materialRed;
            }
        }
    
        private MaterialPropertyBlock _materialWhite;
        private MaterialPropertyBlock MaterialWhite
        {
            get
            {
                if (_materialWhite == null)
                    _materialWhite = new();
                return _materialWhite;
            }
        }

        private readonly int _detailMapColorID = Shader.PropertyToID("_DetailMapColor");

        private void Start()
        {
            MaterialRed.SetColor(_detailMapColorID, Color.red);
            MaterialWhite.SetColor(_detailMapColorID, Color.white);
        }

        public override void OnDamage()
        {
            if (_hitCoroutine != null) return;
            _hitCoroutine = StartCoroutine(ColorChange());
            base.OnDamage();
        }

        IEnumerator ColorChange()
        {
            foreach (Renderer renderer in renderers)
            {
                renderer.SetPropertyBlock(_materialRed);
            }
            
            yield return new WaitForSeconds(.45f);
            
            foreach (Renderer renderer in renderers)
            {
                renderer.SetPropertyBlock(_materialWhite);
            }

            _hitCoroutine = null;
        }

   
    }
}
