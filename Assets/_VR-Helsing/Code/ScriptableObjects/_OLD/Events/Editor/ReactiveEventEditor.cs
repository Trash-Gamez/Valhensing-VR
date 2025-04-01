#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using _VanHelsingVR.Events;

//TODO: GET A WAY TO MAKE A GENERIC EDITOR
[CustomEditor(typeof(ReactiveEvent<>), true)]
public class ReactiveEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Call Event"))
        {
            if (target is IEditorReactiveEvent reactiveEvent) reactiveEvent.RaiseDefaultEvent();
        }
    }
}
#endif
