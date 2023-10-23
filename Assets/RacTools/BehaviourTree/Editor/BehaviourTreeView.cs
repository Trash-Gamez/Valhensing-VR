using System;
using System.Collections.Generic;
using System.Linq;
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
        
        internal void OpenTree(BehaviourTree tree)
        {
            _currentTree = tree;
            PopulateTreeView(_currentTree);
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            AppendDerivedNodeToMenu<DecoratorNode>(evt);
            AppendDerivedNodeToMenu<CompositeNode>(evt);
            AppendDerivedNodeToMenu<ActionNode>(evt);
        }

        private void PopulateTreeView(BehaviourTree tree)
        {
            //TODO: Make this method for multi pages
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;
            _currentTree = tree;
            _currentTree.Nodes.ForEach(node => CreateNodeView(node));
        }

        #region GraphView Changed Handler

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphChangedElements)
        {
            HandleDeletedGraphElements(graphChangedElements.elementsToRemove);
            HandleMovedGraphElements(graphChangedElements.movedElements);
            HandleEdgesCreated(graphChangedElements.edgesToCreate);
            
            return graphChangedElements;
        }
        

        private void HandleMovedGraphElements(List<GraphElement> movedElements)
        {
            
        }

        private void HandleDeletedGraphElements(List<GraphElement> elementsToRemove)
        {
            if (elementsToRemove == null) return;
            Debug.Log("Se eliminan elementos");
            
            foreach (var removedElement in elementsToRemove)
            {
                Debug.Log(elementsToRemove.ToString());
                if(removedElement is not NodeView view) continue;
                
                _currentTree.DeleteNode(view.Node);
            }
        }

        private void HandleEdgesCreated(List<Edge> edgesToCreate)
        {
            
        }
        

        #endregion

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
            var node = _currentTree.CreateNode(nodeType);
            if (node == null) throw new InvalidOperationException($"The type: {nodeType} is not a Valid Node Type");

            CreateNodeView(node);
        }

        private void CreateNodeView(Node node)
        {
            NodeView view = node switch
            {
                RootNode root => new RootNodeView(root),
                CompositeNode composite => new CompositeNodeView(composite),
                ActionNode action => new ActionNodeView(action),
                DecoratorNode decorator => new DecoratorNodeView(decorator),
                _ => throw new ArgumentOutOfRangeException(nameof(node), node, null)
            };
            
            AddElement(view);
        }
    }
}