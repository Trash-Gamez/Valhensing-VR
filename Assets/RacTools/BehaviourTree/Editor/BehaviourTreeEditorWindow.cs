using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Callbacks;
using RacTools.Views;

namespace RacTools.BehaviourTree
{
    public class BehaviourTreeEditorWindow : EditorWindow
    {
        private static HashSet<BehaviourTree> _trees = new HashSet<BehaviourTree>();

        private static BehaviourTreeView _treeView;
        private static InspectorView _inspectorView;
        private static IMGUIContainer _blackboardView;

        private static SerializedObject _treeObject;
        private static SerializedProperty _blackBoardProperty;

        private static BehaviourTreeController _treeController = null;
        
        #region Open Window Logic
        [MenuItem("Tools/RacTools/BehaviourTree Window")]
        public static void OpenWindow()
        {
            BehaviourTreeEditorWindow wnd = GetWindow<BehaviourTreeEditorWindow>();
            wnd.titleContent = new GUIContent("BehaviourTree");
        }

        /*
         * Estos metodos se realizan cuando unity detecta que se intenta abrir un asset, es decir, cualquier
         * elemento en la pestaña de "Project"
         */
        
        // Este primer metodo verifica sio oes un arbol de comportamiento el que se intenta abrir 
        // Si no es asi ... Pasa al segundo metodo
        [OnOpenAsset(1)]
        public static bool OpenTree(int instanceID, int line)
        {
            var tree = EditorUtility.InstanceIDToObject(instanceID) as BehaviourTree;
            if (tree == null) return false;
            
            if (!AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID())) return false;
            OpenWindow();
            AddTree(tree);

            return true;
        }
        
        // Este metodo verifica si es un nodo HIJO de un arbol de comportamiento el que se intenta abrir
        // si es asi se abre la ventana
        [OnOpenAsset(2)]
        public static bool OpenNode(int instanceID, int line)
        {
            var node = EditorUtility.InstanceIDToObject(instanceID) as Node;
            if (node == null) return false;
            if (!AssetDatabase.IsSubAsset(node)) return false;
            
            var nodePath = AssetDatabase.GetAssetPath(instanceID);
            var tree = AssetDatabase.LoadMainAssetAtPath(nodePath) as BehaviourTree;
            if (tree == null) return false;

            if (!AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID())) return false;
            OpenWindow();
            AddTree(tree);

            return true;
        }
        

        #endregion
        
        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/RacTools/BehaviourTree/Editor/BehaviourTreeEditorWindow.uxml");
            visualTree.CloneTree(root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/RacTools/BehaviourTree/Editor/BehaviourTreeEditorWindow.uss");
            root.styleSheets.Add(styleSheet);

            _treeView = root.Q<BehaviourTreeView>();
            _treeView.OnNodeSelectionChanged = OnNodeSelectionChanged;
            
            _inspectorView = root.Q<InspectorView>();

            BlackBoardHandler(root);
        }

        private static void BlackBoardHandler(VisualElement root)
        {
            _blackboardView = root.Q<IMGUIContainer>();
            _blackboardView.onGUIHandler = () =>
            {
                if (_treeObject == null) return;
                _treeObject.Update();
                EditorGUILayout.PropertyField(_blackBoardProperty);
                _treeObject.ApplyModifiedProperties();
            };
        }

        #region Multi Tree Selection

        private static void AddTree(BehaviourTree tree)
        {
            if (_trees.Add(tree))
            {
                Debug.Log("Added New Tree");
                AddTreeView(tree);
            }
            else
            {
                Debug.Log("Opening existing tree");
                GetTreeView(tree);
            }
        }

        private static void GetTreeView(BehaviourTree tree)
        {
            Debug.Log("Added Tree View");
            _treeObject = new SerializedObject(tree);
            _blackBoardProperty = _treeObject.FindProperty("blackboard");
            _treeView.PopulateTreeView(tree);
        }

        private static void AddTreeView(BehaviourTree tree)
        {
            //TODO: Add logic of the menu
            _treeObject = new SerializedObject(tree);
            _blackBoardProperty = _treeObject.FindProperty("blackboard");
            _treeView.PopulateTreeView(tree);
        }

        #endregion
        
        private void OnSelectionChange()
        {
            var selectedGameObject = Selection.activeGameObject;
            
            //Opens the Tree View Only if the selected gameObject is not null
            //The selected GameObject has a BehaviourTreeController Script Attached
            //The BehaviourTree in the BehaviourTreeController is not null
            if (selectedGameObject 
                && selectedGameObject.TryGetComponent<BehaviourTreeController>(out var treeController)
                && treeController.behaviourTree != null)
            {
                GetTreeView(treeController.behaviourTree);
                return;
            }
            
            //OtherWise Pupulate with the first tree in the _trees HashSet
            PopulateWithFirstTree();
        }

        private void PopulateWithFirstTree()
        {
            var tree = _trees.FirstOrDefault();
            if (!tree) return;
            var canOpen = Application.isPlaying ? true : AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID());
            
            if(tree && canOpen) GetTreeView(tree);
        }

        private void OnNodeSelectionChanged(Node node)
        {
            _inspectorView.OnSelectedObject(node);
        }
    }
}