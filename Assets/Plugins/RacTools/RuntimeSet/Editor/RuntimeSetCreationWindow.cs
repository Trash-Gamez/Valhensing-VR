using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace RacTools.RuntimeSet.Editor
{
    public class RuntimeSetCreationWindow : EditorWindow
    {
        private string CreationType = String.Empty;

        private List<TextAsset> _runtimeSetsScripts = new();
    
        [MenuItem("Window/RuntimeSet")]
        public static void ShowWindow()
        {
        
            GetWindow<RuntimeSetCreationWindow>("Create A Runtime Set");
        
            var assets = AssetDatabase.FindAssets("RuntimeSet t:MonoScript l:ScriptableObject");
            var currentTypes = TypeCache.GetTypesDerivedFrom(typeof(RuntimeSet<>));
        

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
}
