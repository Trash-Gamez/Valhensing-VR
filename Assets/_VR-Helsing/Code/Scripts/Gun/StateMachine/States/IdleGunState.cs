using RacTools.StateMachine;

namespace _VR_Helsing.Gun
{
    public class IdleGunState : BaseGunState
    {
        public IdleGunState(GunStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            
        }
    }
}