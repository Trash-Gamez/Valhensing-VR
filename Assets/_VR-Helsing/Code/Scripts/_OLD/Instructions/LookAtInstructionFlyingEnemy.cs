using System;
using RacTools.Variables;
using UnityEngine;

namespace _VanHelsingVR.Instructions
{
    public class LookAtInstructionFlyingEnemy : MonoBehaviour
    {
        [SerializeField] private VariableReference<Transform> target;
        [SerializeField] private bool usePosAndDamp;
        
        private void Update()
        {
            if (usePosAndDamp)
            {
                var targetPos = target.Value.position;
                targetPos.y = transform.position.y;
                transform.LookAt(targetPos);
            }
            else
            {
                transform.LookAt(target.Value);
            }
        }
    }
}
