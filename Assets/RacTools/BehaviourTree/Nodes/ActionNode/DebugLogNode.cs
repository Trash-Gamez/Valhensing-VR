using UnityEngine;

namespace RacTools.BehaviourTree
{
    public class DebugLogNode : ActionNode
    {
        public string message;
        protected override void OnStart()
        {
            Debug.Log($"OnStart: {message}");
        }

        protected override State OnTick()
        {
            Debug.Log($"OnUpdate: {message}");
            return State.Success;
        }

        protected override void OnStop()
        {
            Debug.Log($"OnStop: {message}");
        }
    }
}