using System;
using System.Collections.Generic;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public class RootNode : Node
    {
        [HideInInspector] public Node Child = null;

        public override Node Clone()
        {
            var node = Instantiate(this);
            if (Child != null)
                node.Child = Child.Clone();
            return node;
        }

        protected override void OnStart(){}

        protected override State OnTick()
        {
            return Child != null ? Child.Tick() : State.Failure;
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
            if (Child == null) return null;
            
            return new List<Node>()
            {
                Child
            };
        }
    }
}