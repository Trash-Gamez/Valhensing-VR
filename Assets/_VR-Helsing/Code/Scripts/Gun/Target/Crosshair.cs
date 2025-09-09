using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _VR_Helsing.Gun.Target
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] crossHairSpriteRenderers;
        [SerializeField] private Animator crosshairAnimator;
        
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
            SetCrossHairColor(newProfile.CrosshairColor);
            
        }
    }
}