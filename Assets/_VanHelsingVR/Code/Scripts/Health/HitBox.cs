using System;
using System.Collections.Generic;
using UnityEngine;

namespace _VanHelsingVR.Health
{
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class HitBox : MonoBehaviour
    {
        [SerializeField] private List<HitBox> children = new();
        private HashSet<Collider> _insideColliders = new();
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Entro en esta hitbox");
            
            if (!_insideColliders.Add(other)) return;
        }

        private void OnTriggerExit(Collider other)
        {
            _insideColliders.Remove(other);
            if(_insideColliders.Count <= 0)
                Disable();
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