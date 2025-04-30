using RacTools.StateMachine;

namespace _VR_Helsing.Gun
{
    public abstract class BaseGunState : BaseState
    {
        protected readonly GunStateMachine GunStateMachine;
        protected readonly GunStateFactory StateFactory;
        
        protected BaseGunState(GunStateMachine stateMachine, GunStateFactory stateFactory) : base(stateMachine)
        {
            GunStateMachine = stateMachine;
            StateFactory = stateFactory;
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