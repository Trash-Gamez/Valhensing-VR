namespace RacTools.StateMachine
{
    public abstract class BaseState 
    {
        private static int _lastID;
        public int IDState { get; set; }
        
        
        protected BaseStateMachine _machine;

        public BaseState(BaseStateMachine stateMachine)
        {
            IDState = _lastID++;
        }

        public abstract void Enter();

        public abstract void Update();

        public abstract void FixedUpdate();

        public abstract void Exit();
    }
}