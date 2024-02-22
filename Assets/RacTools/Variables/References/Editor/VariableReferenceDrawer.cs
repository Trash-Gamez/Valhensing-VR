using System;
using UnityEditor;
using UnityEngine;
using RacTools.Editor;

namespace RacTools.Variables.Editor
{
    //Esta clase solo es para dibujar las referencias 
    [CustomPropertyDrawer(typeof(VariableReference<>))]
    public class VariableReferenceDrawer : RacPropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
            
            //Obtenemos la propiedad del tipo de variable en la clase "VariableReference"
            var variableTypeProp = property.FindPropertyRelative("variableType");
            
            //La tranformamos a el enumarador que define el tipo de variable
            var variableType =  (VariableReference.VariableType)variableTypeProp.enumValueIndex;
            
            //Se obtiene la propiedad dependiendo el tipo de variable que sea el actual
            var serializedPropertyVariable = variableType switch
            {
                VariableReference.VariableType.Constant => property.FindPropertyRelative("constant"),
                VariableReference.VariableType.Reference => property.FindPropertyRelative("reference"),
                VariableReference.VariableType.Instance => property.FindPropertyRelative("reference"),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            //Se define un estado
            var headerStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.UpperCenter
            };
            
            //DRAWER
            EditorGUI.BeginProperty(position, label, property);
            DrawLabel(position, label.text + " Reference", headerStyle);
            DrawProperty(position, variableTypeProp);
            DrawProperty(position, serializedPropertyVariable);
            Space();
            EditorGUI.EndProperty();
        }
    }
}