using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class BehaviourTreeEditorWindow : EditorWindow
{
    [MenuItem("Tools/RacTools/BehaviourTree Window")]
    public static void OpenWindow()
    {
        BehaviourTreeEditorWindow wnd = GetWindow<BehaviourTreeEditorWindow>();
        wnd.titleContent = new GUIContent("BehaviourTree");
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
}