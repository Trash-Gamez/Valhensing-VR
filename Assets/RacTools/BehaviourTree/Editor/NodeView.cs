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
            viewDataKey = node.guid;

            style.left = node.pos.x;
            style.top = node.pos.y;
        }

        public override void SetPosition(Rect newPos)
        {
            newPos.x = Mathf.Clamp(newPos.x, -500, 500);
            newPos.y = Mathf.Clamp(newPos.y, -500, 500);
            Node.pos.x = newPos.x;
            Node.pos.y = newPos.yMin;
            base.SetPosition(newPos);
        }
    }
}
