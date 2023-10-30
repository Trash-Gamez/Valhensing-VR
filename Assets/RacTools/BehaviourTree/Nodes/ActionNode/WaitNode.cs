using UnityEngine;

namespace RacTools.BehaviourTree
{
    public class WaitNode : ActionNode
    {
        public float waitTime;
        private float _startTime; 
        protected override void OnStart()
        {
            _startTime = Time.time;
        }

        protected override State OnTick()
        {
            //Si el tiempo que transcurrido es mayor a el tiempo de espera se retorna un success y se termina
            //Si no sigue transcurriendo
            return Time.time - _startTime > waitTime ? State.Success : State.Running;
        }

        protected override void OnStop()
        {
            
        }
    }
}