using System;
using UnityEditor;
using UnityEngine;
using RacTools.Editor;

namespace RacTools.Variables.Editor
{
    [CustomPropertyDrawer(typeof(VariableReference<>))]
    public class VariableReferenceDrawer : RacPropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
            
            //PROPERTIES
            var variableTypeProp = property.FindPropertyRelative("variableType");

            var variableType =  (VariableReference.VariableType)variableTypeProp.enumValueIndex;

            var serializedPropertyVariable = variableType switch
            {
                VariableReference.VariableType.Constant => property.FindPropertyRelative("constant"),
                VariableReference.VariableType.Reference => property.FindPropertyRelative("reference"),
                VariableReference.VariableType.Instance => property.FindPropertyRelative("reference"),
                _ => throw new ArgumentOutOfRangeException()
            };

            var headerStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.UpperCenter
            };


            //headerStyle.font.material.color = Color.green;
            //DRAWER
            EditorGUI.BeginProperty(position, label, property);
            DrawLabel(position, label, headerStyle);
            DrawProperty(position, variableTypeProp);
            DrawProperty(position, serializedPropertyVariable);
            EditorGUI.EndProperty(); 
            
        }
    }
}