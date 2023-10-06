using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeGraph : EditorWindow
{
    private BehaviourTreeGraphView _graphView;
    
    [MenuItem("Tools/RacTools/Behaviour Tree Graph")]
    public static void OpenBehaviourGraphWindow()
    {
        var window = GetWindow<BehaviourTreeGraph>();
        window.titleContent = new GUIContent("Behaviour Tree Graph");
    }
    
    private void ConstructuGraphView()
    {
        _graphView = new BehaviourTreeGraphView
        {
            name = "Behaviour Tree Graph"
        };
        
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var nodeCreateButton = new UnityEngine.UIElements.Button();
        nodeCreateButton.clicked += () =>
        {
            _graphView.CreateNode("Behaviour Node");
        };
        nodeCreateButton.text = "Creat Node";
        
        toolbar.Add(nodeCreateButton);
        
        rootVisualElement.Add(toolbar);
    }

    private void OnEnable()
    {
        ConstructuGraphView();
        GenerateToolbar();
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
