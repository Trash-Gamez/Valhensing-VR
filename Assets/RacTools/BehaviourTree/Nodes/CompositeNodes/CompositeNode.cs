using System.Collections.Generic;

namespace RacTools.BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        public List<Node> Children { get; set; } = new List<Node>();
    }
}