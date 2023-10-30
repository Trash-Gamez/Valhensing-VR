using System.Collections.Generic;

namespace RacTools.BehaviourTree
{
    public abstract class ActionNode : Node
    {
        public override void AddChild(Node child){}

        public override void RemoveChild(Node child){}

        public override List<Node> GetChildren() => null;
    }
}