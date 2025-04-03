using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class FlyingAnimationEvents : MonoBehaviour
    {
        [SerializeField] private FlyingEnemyStateMachine stateMachine;

        public void Shoot()
        {
            stateMachine.Shoot();
        }

        public void EndAttack()
        {
            stateMachine.AttackEnd();
        }

        public void OnDeadAnimationEnd()
        {
            stateMachine.OnDeadAnimationEnd();
        }
    }
}
