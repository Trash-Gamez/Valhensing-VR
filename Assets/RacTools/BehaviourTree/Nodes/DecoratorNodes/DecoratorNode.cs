using System.Collections.Generic;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public abstract class DecoratorNode : Node
    {
        [HideInInspector] public Node Child;
        
        public override Node Clone()
        {
            var node = base.Clone() as DecoratorNode;
            if (Child != null)
                node!.Child = Child;
            return node;
        }
        
        public override void AddChild(Node child)
        {
            if (Child != null) return;
            Child = child;
        }

        public override void RemoveChild(Node child)
        {
            if (Child != child) return;
            Child = null;
        }

        public override List<Node> GetChildren() => new List<Node>() { Child };
    }
}