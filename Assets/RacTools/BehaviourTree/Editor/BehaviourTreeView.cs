using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace RacTools.BehaviourTree
{
    public class BehaviourTreeView : GraphView
    {
        private BehaviourTree _currentTree;

        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, UxmlTraits> { }
        public BehaviourTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/RacTools/BehaviourTree/Editor/BehaviourTreeEditorWindow.uss");
            styleSheets.Add(styleSheet);
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            Debug.Log("Populate Context menu");
            AppendDerivedNodeToMenu<DecoratorNode>(evt);
            AppendDerivedNodeToMenu<CompositeNode>(evt);
            AppendDerivedNodeToMenu<ActionNode>(evt);
        }

        internal void PopulateTreeView(BehaviourTree tree)
        {
            //TODO: Make this method for multi pages
            DeleteElements(graphElements);
            _currentTree = tree;
            _currentTree.Nodes.ForEach(node => CreateNodeView(node));
        }

        private void AppendDerivedNodeToMenu<TNode>(ContextualMenuPopulateEvent e) where TNode : Node
        {
            var status = _currentTree == null ? DropdownMenuAction.Status.Disabled : DropdownMenuAction.Status.Normal;
            var types = TypeCache.GetTypesDerivedFrom<TNode>();
            e.menu.AppendSeparator("Create/");
            
            foreach (var type in types)
            {
                e.menu.AppendAction($"Create/[{type.BaseType}] {type.Name}", (d) => CreateGraphNode(type), status);
            }
        }

        private void CreateGraphNode(Type nodeType)
        {
            if (_currentTree == null) throw new ArgumentNullException("There is no Current Tree Selected");
            Node node = _currentTree.CreateNode(nodeType);
            if (node == null) throw new InvalidOperationException($"The type: {nodeType} is not a Valid Node Type");

            CreateNodeView(node);
        }

        private void CreateNodeView(Node node)
        {
            NodeView view = new NodeView(node);
            AddElement(view);
        }
    }
}