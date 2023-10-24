using System;
using System.Collections.Generic;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public class RootNode : Node
    {
        public Node Child;

        protected override void OnStart(){}

        protected override State OnUpdate()
        {
            return Child != null ? Child.Update() : State.Failure;
        }

        protected override void OnStop()
        {
                
        }

        public override void AddChild(Node child)
        {
            if (Child != null) return;
            Child = child;
        }

        public override void RemoveChild(Node child)
        {
            if (child != Child) return;
            Child = null;
        }

        public override List<Node> GetChildren()
        {
            return new List<Node>()
            {
                Child
            };
        }
    }
}