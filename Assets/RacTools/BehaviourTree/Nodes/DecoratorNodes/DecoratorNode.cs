using System.Collections.Generic;

namespace RacTools.BehaviourTree
{
    public abstract class DecoratorNode : Node
    {
        public Node Child;
        
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