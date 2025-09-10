using System;
using UnityEngine;

namespace _VR_Helsing.Gun.Target
{
    public class Crosshair : MonoBehaviour
    {
        private static readonly int _IsActive = Animator.StringToHash("isActive");
        
        
        [SerializeField] private SpriteRenderer[] crossHairSpriteRenderers;
        [SerializeField] private Animator crosshairAnimator;
        private static readonly int _IsPulseActive = Animator.StringToHash("isPulseActive");

        private void Start()
        {
            transform.parent = null;
        }

        public void SetCrosshairActive(bool value)
        {
            for (int i = 0; i < crossHairSpriteRenderers.Length; i++)
            {
                crossHairSpriteRenderers[i].enabled = value;
            }
        }

        public void SetCrossHairColor(Color newColor)
        {
            for (int i = 0; i < crossHairSpriteRenderers.Length; i++)
            {
                crossHairSpriteRenderers[i].color = newColor;
            }
        }

        public void SetTargetProfile(TargetProfile newProfile)
        {
            if (newProfile.TargetType == TargetType.None)
            {
                ResetCrosshair();
                return;
            }
                
            SetCrossHairColor(newProfile.CrosshairColor);
            crosshairAnimator.SetBool(_IsActive, newProfile.IsHostile);
            crosshairAnimator.SetBool(_IsPulseActive, newProfile.IsInteractable);
        }

        private void ResetCrosshair()
        {
            for (int i = 0; i < crossHairSpriteRenderers.Length; i++)
            {
                crossHairSpriteRenderers[i].color = Color.white;
            }
            crosshairAnimator.SetBool(_IsActive, false);
            crosshairAnimator.SetBool(_IsPulseActive, false);
        }
    }
}