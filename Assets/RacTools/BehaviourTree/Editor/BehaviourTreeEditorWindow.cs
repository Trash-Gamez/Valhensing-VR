using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace RacTools.BehaviourTree
{
    public class BehaviourTreeEditorWindow : EditorWindow
    {
        [MenuItem("Tools/RacTools/BehaviourTree Window")]
        public static void OpenWindow()
        {
            BehaviourTreeEditorWindow wnd = GetWindow<BehaviourTreeEditorWindow>();
            wnd.titleContent = new GUIContent("BehaviourTree");
        }

        [OnOpenAsset(1)]
        public static bool OpenTree(int instanceID, int line)
        {
            var tree = EditorUtility.InstanceIDToObject(instanceID) as BehaviourTree;
            if (tree == null) return false;
            
            OpenWindow();
            //TODO: Make Tree View Populate with this tree
            return true;
        }
        
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
            //TODO: Make Tree View Populate this tree
            
            return true;
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/RacTools/BehaviourTree/Editor/BehaviourTreeEditorWindow.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/RacTools/BehaviourTree/Editor/BehaviourTreeEditorWindow.uss");
            root.styleSheets.Add(styleSheet);
        }

        private void OnSelectionChange()
        {
            var tree = Selection.activeObject as BehaviourTree;
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