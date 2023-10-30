using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public abstract class DecoratorNode : Node
    {
        [HideInInspector] public Node Child;
        
        public override Node Clone()
        {
            var node = Instantiate(this);
            if (Child != null)
                node.Child = Child.Clone();
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

        public override List<Node> GetChildren()
        {
            if(Child == null) return null;
            return new List<Node>() { Child };
        }
    }
}