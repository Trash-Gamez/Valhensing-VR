using System;
using UnityEngine;

namespace _VanHelsingVR.Health
{
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class HitBox : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            
        }

        public void Enable()
        {
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
        }
    }
}