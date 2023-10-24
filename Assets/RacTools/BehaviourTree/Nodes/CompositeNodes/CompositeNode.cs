using System.Collections.Generic;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        
        [HideInInspector] public List<Node> Children = new List<Node>();

        public override Node Clone()
        {
            var node = base.Clone() as CompositeNode;
            node.Children = Children.ConvertAll(n => node.Clone());
            return node;
        }

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