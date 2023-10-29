using System;

namespace RacTools.BehaviourTree
{
    public class Invert : DecoratorNode
    {
        protected override void OnStart()
        {
            
        }

        protected override State OnTick()
        {
            return Child.Tick() switch
            {
                State.Running => State.Running,
                State.Failure => State.Success,
                State.Success => State.Failure,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override void OnStop()
        {
            
        }
    }
}