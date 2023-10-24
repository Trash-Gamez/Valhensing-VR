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
        internal Action<Node> OnNodeSelectionChanged;
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
            
            NodeView.OnNodeSelected -= OnSelectedNodeChanged;
            NodeView.OnNodeSelected += OnSelectedNodeChanged;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            AppendDerivedNodeToMenu<DecoratorNode>(evt);
            AppendDerivedNodeToMenu<CompositeNode>(evt);
            AppendDerivedNodeToMenu<ActionNode>(evt);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports
                .ToList()
                .Where(endPort => 
                    endPort.direction != startPort.direction
                    && endPort.node != startPort.node)
                .ToList();
        }

        internal void PopulateTreeView(BehaviourTree tree)
        {
            //TODO: Make this method for multi pages
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;
            
            _currentTree = tree;
            
            //Creates the root Node if its null
            if (_currentTree.Root == null)
            {
                _currentTree.Root = _currentTree.CreateNode<RootNode>();
                EditorUtility.SetDirty(_currentTree);
                AssetDatabase.SaveAssets();
            }
            
            //Create The Node Views
            foreach (var node in _currentTree.Nodes)
            {
                CreateNodeView(node);
            }
            
            //Create The Edges
            foreach (var node in _currentTree.Nodes)
            {
                var children = node.GetChildren();
                if(children == null) continue;
                if(!children.Any()) continue;
                Debug.Log($"Hay hijos en: {node.name}");
                
                children.ForEach(child =>
                {
                    if (child == null) return;
                    NodeView parentView = GetNodeByGuid(node.guid) as NodeView;
                    NodeView childView = GetNodeByGuid(child.guid) as NodeView;

                    var edge = parentView!.outputPort.ConnectTo(childView!.inputPort);
                    AddElement(edge);
                });
            }
        }

        #region GraphView Changed Handler
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphChangedElements)
        {
            HandleDeletedGraphElements(graphChangedElements.elementsToRemove);
            HandleMovedGraphElements(graphChangedElements.movedElements);
            HandleEdgesCreated(graphChangedElements.edgesToCreate);
            
            return graphChangedElements;
        }
        
        #region Handlers
        
        /// <summary>
        /// Handles The Deleted Elements In The Tree View.
        /// Deletes All The Nodes That Where Removed
        /// </summary>
        /// <param name="elementsToRemove">The list of removed elements</param>
        private void HandleDeletedGraphElements(List<GraphElement> elementsToRemove)
        {
            if (elementsToRemove == null) return;
            
            foreach (var removedElement in elementsToRemove)
            {
                switch (removedElement)
                {
                    case NodeView view:
                        HandleDeletedNodeView(view);
                        continue;
                    case Edge edge:
                        HandleDeletedEdge(edge);
                        continue;
                    default:
                        continue;
                }
            }
        }
        
        //Handles All The Deleted Edges
        private void HandleDeletedEdge(Edge edgeRemoved)
        {
            NodeView parentView = edgeRemoved.output.node as NodeView;
            NodeView childView = edgeRemoved.input.node as NodeView;

            if (parentView == null || childView == null)
            {
                Debug.LogError("One of th edges created is no compatible");
                return;
            }
            
            parentView.Node.RemoveChild(childView.Node);
        }
        
        //Handles All The Deleted Node Views
        private void HandleDeletedNodeView(NodeView nodeView)
        {
            _currentTree.DeleteNode(nodeView.Node);
        }

        /// <summary>
        /// Handles The Edge Creation From The Tree View.
        /// Creates Children From The Nodes And Edges That Where Created
        /// </summary>
        /// <param name="edgesToCreate">The edges that were created</param>
        private void HandleEdgesCreated(List<Edge> edgesToCreate)
        {
            if (edgesToCreate == null) return;
            foreach (var edge in edgesToCreate)
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                if (parentView == null || childView == null)
                {
                    Debug.LogError("One of th edges created is no compatible");
                    continue;
                }
                
                parentView.Node.AddChild(childView.Node);
            }
        }
        private void HandleMovedGraphElements(List<GraphElement> movedElements)
        {
            
        }

        #endregion
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

        private void OnSelectedNodeChanged(NodeView nodeView)
        {
            OnNodeSelectionChanged?.Invoke(nodeView.Node);
        }
    }
}