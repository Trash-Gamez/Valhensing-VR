using RacTools.StateMachine;

namespace _VR_Helsing.Gun
{
    public class ReloadGunState : BaseGunState
    {
        public ReloadGunState(GunStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }
}