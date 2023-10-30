using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public static event Action<NodeView> OnNodeSelected;
        public Node Node {get; private set;}

        public Port inputPort = null;
        public Port outputPort = null;

        protected NodeView(Node node, Port.Capacity? inputCapacity, Port.Capacity? outputCapacity) : 
            base("Assets\\RacTools\\BehaviourTree\\Editor\\NodeDocument.uxml")
        {
            Node = node;
            title = node.name;
            viewDataKey = node.guid;

            style.left = node.pos.x;
            style.top = node.pos.y;

            if (inputCapacity != null)
            { 
               inputPort = CreateInputPort(inputCapacity.Value);
               inputContainer.Add(inputPort);
            }

            if (outputCapacity != null)
            {
                outputPort = CreateOutputPort(outputCapacity.Value);
                outputContainer.Add(outputPort);
            }
        }

        /// <summary>
        /// Create the port that this NodeView is gonna use to input
        /// </summary>
        protected virtual Port CreateInputPort(Port.Capacity capacity)
        {
            var port = InstantiatePort(Orientation.Vertical, Direction.Input, capacity, typeof(Node));
            return port;
        }

        /// <summary>
        /// Create the port that this NodeView is gonna use to output
        /// </summary>
        /// <returns>The number of Ports Created</returns>
        protected virtual Port CreateOutputPort(Port.Capacity capacity)
        {
            var port = InstantiatePort(Orientation.Vertical, Direction.Output, capacity, typeof(Node));
            return port;
        }

        public override void SetPosition(Rect newPos)
        {
            newPos.x = Mathf.Clamp(newPos.x, -1000, 1000);
            newPos.y = Mathf.Clamp(newPos.y, -1000, 1000);
            Node.pos.x = newPos.xMin;
            Node.pos.y = newPos.yMin;
            base.SetPosition(newPos);
        }

        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }
    }

    public sealed class RootNodeView : NodeView
    {
        public RootNodeView(RootNode node) : base(node, null, Port.Capacity.Single)
        {
            //Deletes the capabilitie of the node of being deleted
            capabilities &= ~Capabilities.Deletable;
        }
    }

    public sealed class CompositeNodeView : NodeView
    {
        public CompositeNodeView(CompositeNode node) : base(node, Port.Capacity.Single, Port.Capacity.Multi)
        {
        }
    }
    
    public sealed class DecoratorNodeView : NodeView
    {
        public DecoratorNodeView(DecoratorNode node) : base(node, Port.Capacity.Single, Port.Capacity.Single)
        {
        }
    }
    
    public sealed class ActionNodeView : NodeView
    {
        public ActionNodeView(ActionNode node) : base(node, Port.Capacity.Single, null)
        {
        }
    }
}
