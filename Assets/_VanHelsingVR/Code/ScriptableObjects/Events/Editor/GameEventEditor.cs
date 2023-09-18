#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using _VanHelsingVR.Events;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Call Event"))
        {
            var gameEvent = target as GameEvent;
            if (gameEvent == null) throw new Exception("GameEvent Is Null");
            gameEvent.Raise();
        }
    }
}
#endif
