using RacTools.StateMachine;

namespace _VR_Helsing.Gun
{
    public abstract class BaseGunState : BaseState
    {
        protected GunStateMachine gunStateMachine;
        
        protected BaseGunState(GunStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Update()
        {
            CheckState();
        }

        public override void FixedUpdate(){}

        public override void CheckState()
        {}
    }
}