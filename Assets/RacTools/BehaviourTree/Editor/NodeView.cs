using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Node Node {get; private set;}

        public NodeView(Node node)
        {
            Node = node;
            title = node.name;
        }
    }
}
