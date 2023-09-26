using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;


public class RuntimeSetCreationWindow : EditorWindow
{
    private string CreationType = String.Empty;

    private List<TextAsset> _runtimeSetsScripts = new();
    
    [MenuItem("Window/RuntimeSet")]
    public static void ShowWindow()
    {
        
        GetWindow<RuntimeSetCreationWindow>("Create A Runtime Set");
        
        var assets = AssetDatabase.FindAssets("RuntimeSet t:MonoScript l:ScriptableObject");

        assets.ForEach(scriptable =>
        {
            var path = AssetDatabase.GUIDToAssetPath(scriptable);
            Debug.Log(path);
            var asset = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));
            if (asset == null) return;
                
            Debug.Log(asset.name);
            
            asset.name.Split("RuntimeSet");
        });
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Select the type to create");

        if (GUILayout.Button("Prueba"))
        {
            
        }
    }

    private void CreateRuntime()
    {
        
    }
}
