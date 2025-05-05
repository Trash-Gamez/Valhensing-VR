using UnityEngine;

namespace _VR_Helsing.Gun.Animation
{
    public class ReloadBehaviour : StateMachineBehaviour
    {
        public delegate void OnClipLenght(float clipLenght); 
        public event OnClipLenght OnClipLenghtGet;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            OnClipLenghtGet?.Invoke(stateInfo.length);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }
    }
}