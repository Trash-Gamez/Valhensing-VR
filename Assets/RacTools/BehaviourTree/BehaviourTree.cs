using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace RacTools.BehaviourTree
{
    [CreateAssetMenu(fileName = "BehaviourTree", menuName = "RacTools/BehaviourTree")]
    public class BehaviourTree : ScriptableObject
    {
        private Node _root;
        public Node.State TreeState { get; private set; } = Node.State.Running;
        
        public List<Node> Nodes { get; private set; } = new List<Node>();

        public Node.State Update()
        {
            if (TreeState == Node.State.Running)
            {
                TreeState = _root.Update();
            }

            return TreeState;
        }

        #if UNITY_EDITOR
        public Node CreateNode<TNode>() where TNode : Node, new()
        {
            Node node = CreateInstance<TNode>();
            node.name = typeof(TNode).Name;
            node.guid = GUID.Generate().ToString();
            Nodes.Add(node);
            
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

        private static Node _defaultNode = null;

        [MenuItem("Assets/Add Default Node", true)]
        private static bool AddDefaultNodeValidation()
        {
            return Selection.activeObject is BehaviourTree;
        }
        
        [MenuItem("Assets/Removes Default Node", true)]
        private static bool RemoveDefaultNodeValidation()
        {
            return Selection.activeObject is BehaviourTree;
        }
        
        [MenuItem("Assets/Add Default Node")]
        private static void AddDefaultNode()
        {
            if (_defaultNode != null) return;
            var tree = Selection.activeObject as BehaviourTree;
            _defaultNode = tree!.CreateNode<DebugLogNode>();
        }
        
        [MenuItem("Assets/Removes Default Node")]
        private static void RemoveDefaultNode()
        {
            if (_defaultNode == null) return;
            var tree = Selection.activeObject as BehaviourTree;
            tree!.DeleteNode(_defaultNode);
        }
        #endif
    }
}