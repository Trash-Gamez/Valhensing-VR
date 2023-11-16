using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class GiantEnemyStateMachine : EnemyStateMachine
    {
        public static readonly int IsAttackingAnimID = Animator.StringToHash("IsAtacking");
        public static readonly int IsWalkingAnimID = Animator.StringToHash("IsWalking");
        public static readonly int IsDeadAnimID = Animator.StringToHash("IsDead");
        
        public override void RestartAnimatorParams()
        {
            Animator.SetBool(IsAttackingAnimID, false);
            Animator.SetBool(IsWalkingAnimID, false);
            Animator.SetBool(IsDeadAnimID, false);
        }
    }
}