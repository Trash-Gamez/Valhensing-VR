using System.Collections;
using System.Collections.Generic;
using RacTools.Variables;
using UnityEngine;
using UnityEngine.Assertions;

namespace RacTools.BehaviourTree
{
    public class FollowTargetNode : ActionNode
    {
        [SerializeField] private Variable<Transform> transform;
        protected override void OnStart()
        {
            Assert.IsNotNull(transform);
        }

        protected override State OnTick()
        {
            if (transform == null) return State.Failure;

            
            return State.Success;
        }

        protected override void OnStop()
        {
            throw new System.NotImplementedException();
        }
    }
}
