using System.Collections;
using UnityEngine;
using _VanHelsingVR.Interaction;
using UnityEngine.Serialization;

namespace _VanHelsingVR.Enemy
{
    
    public class Enemy : Hittable
    {
        [SerializeField] Renderer[] renderers;

        [SerializeField] protected float invincibleFrames;

        private Coroutine _hitCoroutine = null;
        private Coroutine _changeColorCoroutine = null;

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
            _hitCoroutine = StartCoroutine(DamageCoroutine());
            
            if(_changeColorCoroutine != null)
                StopCoroutine(_changeColorCoroutine);
            
            _changeColorCoroutine = StartCoroutine(ColorChange());
            base.OnDamage();
        }

        IEnumerator DamageCoroutine()
        {
            var passedFrames = 0;
            while (passedFrames < invincibleFrames)
            {
                passedFrames ++;
                yield return null;
            }
            _hitCoroutine = null;
        }

        IEnumerator ColorChange()
        {
            foreach (Renderer renderer in renderers)
            {
                renderer.SetPropertyBlock(_materialRed);
            }
            
            yield return new WaitForSeconds(.15f);
            
            foreach (Renderer renderer in renderers)
            {
                renderer.SetPropertyBlock(_materialWhite);
            }

            _changeColorCoroutine = null;
        }

   
    }
}
