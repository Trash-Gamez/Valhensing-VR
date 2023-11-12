using System;
using RacTools.Variables;
using UnityEngine;

namespace _VanHelsingVR.Instructions
{
    public class LookAtInstructionFlyingEnemy : MonoBehaviour
    {
        [SerializeField] private VariableReference<Transform> target;
        
        private void Update()
        {
            var targetPos = target.Value.position;
            targetPos.y = transform.position.y;
            
            transform.LookAt(target.Value);
        }
    }
}
