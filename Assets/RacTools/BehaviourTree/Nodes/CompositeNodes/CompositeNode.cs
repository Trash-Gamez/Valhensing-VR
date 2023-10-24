using System.Collections.Generic;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        [field: SerializeField]
        public List<Node> Children { get; set; } = new List<Node>();
        
        public override void AddChild(Node child)
        {
            if (Children.Contains(child)) return;
            Children.Add(child);
        }

        public override void RemoveChild(Node child)
        {
            if (!Children.Contains(child)) return;
            Children.Remove(child);
        }

        public override List<Node> GetChildren() => new List<Node>(Children);
    }
}