using _VanHelsingVR.Conditions;
using _VanHelsingVR.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _VanHelsingVR.Animation
{
    public class HandEffectsController : MonoBehaviour
    {
        [Title("Input")][SerializeField]
        private HandInputReference handInput;

        [Title("Effects Reference")] 
        [SerializeField] private bool useEffects;
        [SerializeField, ShowIf(nameof(useEffects))] private Renderer meshRenderer;
        [SerializeField, ShowIf(nameof(useEffects))] private ParticleSystem particleSystem;
        [SerializeField, ShowIf(nameof(useEffects))] Animator handAnimator;
        [SerializeField , ShowIf(nameof(useEffects))] private ConditionPool canActiveEffects;
    
        private MaterialPropertyBlock _mpb;
        private readonly int _alphaID = Shader.PropertyToID("_Alpha");

        private readonly int _triggerHash = Animator.StringToHash("Trigger");
        private readonly int _gripHash = Animator.StringToHash("Grip");
    
        private bool _areEffectsActive = true;
    
        private void Start()
        {
            if (!useEffects) return;
            InitializeEffects();
        }

        private void InitializeEffects()
        {
            _mpb = new MaterialPropertyBlock();
            DeactivateEffects();
        }

        void Update()
        {
            HandleAnimations();
            if (!useEffects) return;
            HandleEffects();
        }
    
        private void HandleAnimations()
        {
            handAnimator.SetFloat(_triggerHash, handInput.Hand.ActiveInput);
            handAnimator.SetFloat(_gripHash, handInput.Hand.SelectionInput);
        }

        private void HandleEffects()
        {
            var isActivatingEffects = canActiveEffects && handInput.Hand.IsClosed;
        
            if (isActivatingEffects && !_areEffectsActive)
                ActivateEffects();
        
        
            else if(!isActivatingEffects && _areEffectsActive)
                DeactivateEffects();
        }

        private void DeactivateEffects()
        {
            _mpb.SetFloat(_alphaID, 0);
            meshRenderer.SetPropertyBlock(_mpb);
            particleSystem.Stop();
            _areEffectsActive = false;
        }

        private void ActivateEffects()
        {
            _mpb.SetFloat(_alphaID, 1);
            meshRenderer.SetPropertyBlock(_mpb);
            particleSystem.Play();
            AudioManager.Instance.PlaySound3D("FireFist", transform.position);
            _areEffectsActive = true;
        }

        public void Grap(string nombre)
        {
            handAnimator.Play(nombre);
        }
    }
}
