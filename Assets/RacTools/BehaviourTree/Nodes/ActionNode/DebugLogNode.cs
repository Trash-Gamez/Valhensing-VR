using UnityEngine;

namespace RacTools.BehaviourTree
{
    public class DebugLogNode : ActionNode
    {
        public string message;
        protected override void OnStart()
        {
            
        }

        protected override State OnTick()
        {
            Debug.Log(message);
            return State.Success;
        }

        protected override void OnStop()
        {
            
        }
    }
}