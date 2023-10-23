using System;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public class RootNode : Node
    {
        [SerializeField] private BehaviourTree tree;
        public Node Child { get; set; }

        private void OnEnable()
        {
            
        }

        protected override void OnStart(){}

        protected override State OnUpdate()
        {
            return Child != null ? Child.Update() : State.Failure;
        }

        protected override void OnStop()
        {
            throw new System.NotImplementedException();
        }
    }
}