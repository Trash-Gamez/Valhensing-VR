using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        [HideInInspector] public List<Node> Children = new List<Node>();

        public override Node Clone()
        {
            var node = Instantiate(this);
            var clonedChildren = new List<Node>();
            foreach (var child in Children)
            {
                clonedChildren.Add(child.Clone());
            }
            node.Children = clonedChildren;
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

        public void SortChildrenByPos()
        {
            Children = Children.OrderBy(n => n.pos.x).ToList();
        }

        public override List<Node> GetChildren() => new List<Node>(Children);
    }
}