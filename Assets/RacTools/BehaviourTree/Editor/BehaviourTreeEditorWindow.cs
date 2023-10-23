using System;
using System.Collections.Generic;
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
            
            OpenWindow();
            AddTree(tree);

            return true;
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/RacTools/BehaviourTree/Editor/BehaviourTreeEditorWindow.uxml");
            visualTree.CloneTree(root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/RacTools/BehaviourTree/Editor/BehaviourTreeEditorWindow.uss");
            root.styleSheets.Add(styleSheet);

            _treeView = root.Q<BehaviourTreeView>();
            _inspectorView = root.Q<InspectorView>();
        }

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
            _treeView.OpenTree(tree);
        }

        private static void AddTreeView(BehaviourTree tree)
        {
            //TODO: Add logic of the menu
            _treeView.OpenTree(tree);
        }

        private void OnSelectionChange()
        {
            var tree = Selection.activeObject as BehaviourTree;
            
            //TODO: Make this code to open a tree if there is no active tree in window
            if (tree != null)
            {
                Debug.Log("Tengo un arbol seleccionado");
                
            }
            else
            {
                Debug.Log("No hay arbol seleccionado");
            }
        }
    }
}