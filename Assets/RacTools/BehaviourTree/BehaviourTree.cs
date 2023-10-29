using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;

namespace RacTools.BehaviourTree
{
    [CreateAssetMenu(fileName = "BehaviourTree", menuName = "RacTools/BehaviourTree")]
    public class BehaviourTree : ScriptableObject
    {
        [HideInInspector]
        public RootNode Root;

        [field: SerializeField]
        public List<Node> Nodes { get; private set; } = new();
        
        public Node.State TreeState { get; private set; } = Node.State.Running;
        
        //TODO: Calculate the node in action an calculate the next one only when 
        public Node CurrentNode { get; private set; }

        public Blackboard blackboard = new();

        public Node.State Update()
        {
            if (TreeState == Node.State.Running)
            {
                TreeState = Root.Tick();
            }

            return TreeState;
        }

        public void VisitChildNodes(Node node, Action<Node> selector)
        {
            if (!node) return;
            selector?.Invoke(node);
            
            var children = node.GetChildren();
            if (children == null) return;
            
            foreach (var childNode in children)
            {
                if(childNode == null) continue;
                VisitChildNodes(childNode, selector);
            }
        }

        public BehaviourTree Clone()
        {
            var tree = Instantiate(this);
            //Clone the root and all the child nodes inside RootNode
            tree.Root = Root.Clone() as RootNode;
            
            //Adds the children that were clone on the previews Clone function 
            tree.Nodes = new List<Node>();
            VisitChildNodes(tree.Root, (n) => tree.Nodes.Add(n));
            
            return tree;
        }

        public void Bind()
        {
            VisitChildNodes(Root, node =>
            {
                node.blackboard = blackboard;
            });
        }

        #if UNITY_EDITOR
        public TNode CreateNode<TNode>() where TNode : Node, new()
        {
            //There can't be more than 1 RootNode
            if (typeof(TNode) == typeof(RootNode) && Root != null)
            {
                throw new AggregateException("There is already a RootNode on this tree");
            }
            
            TNode node = CreateInstance<TNode>();
            node.name = typeof(TNode).Name;
            node.guid = GUID.Generate().ToString();
            Nodes.Add(node);
            
            if(!Application.isPlaying)
                AssetDatabase.AddObjectToAsset(node, this);
            
            AssetDatabase.SaveAssets();
            
            return node;
        }

        public Node CreateNode(Type nodeType)
        {
            if (nodeType == typeof(RootNode) && Root != null)
            {
                throw new AggregateException("There is already a RootNode on this tree");
            }
            
            Node node = CreateInstance(nodeType) as Node;
            if(node == null)
            {
                Debug.LogError($"Type of: {nodeType.Name} is not Node Type");
                return null;
            }

            node.name = nodeType.Name;
            node.guid = GUID.Generate().ToString();
            Nodes.Add(node);
            
            if(!Application.isPlaying)
                AssetDatabase.AddObjectToAsset(node, this);
            
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node)
        {
            if (!Nodes.Contains(node)) return;
            Nodes.Remove(node);

            if (!AssetDatabase.Contains(node)) return;
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
        #endif
    }
}