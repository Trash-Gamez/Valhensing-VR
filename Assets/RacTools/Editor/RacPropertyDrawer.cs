using UnityEditor;
using UnityEngine;

namespace RacTools.Editor
{
    public class RacPropertyDrawer : PropertyDrawer
    {
        private float _maxPropertyHeight = 0;
        protected float MaxPropertyHeight = 0;
        
        //TODO: Saber cuando inicia el drawer
        protected virtual void InitDrawer(Rect position, SerializedProperty property, GUIContent label)
        {
            _maxPropertyHeight = 0;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            InitDrawer(position, property, label);
        }
        
        protected Rect DrawProperty(Rect position, SerializedProperty property)
        {
            var propertyPos = new Rect(position);
            propertyPos.y += _maxPropertyHeight;
            
            var height = EditorGUI.GetPropertyHeight(property);
            propertyPos.height = height;
            _maxPropertyHeight += height;
            
            EditorGUI.PropertyField(propertyPos, property);
            return propertyPos;
        }
        
        protected Rect DrawLabel(Rect position, string text)
        {
            return DrawLabel(position, new GUIContent(text),  EditorStyles.label);
        }
        
        protected Rect DrawLabel(Rect position, GUIContent content)
        {
            return DrawLabel(position, content, EditorStyles.label);
        }
        
        protected Rect DrawLabel(Rect position, string text, GUIStyle style)
        {
            return DrawLabel(position, new GUIContent(text), style);
        }
        
        protected Rect DrawLabel(Rect position, GUIContent content, GUIStyle style)
        {
            var labelPos = new Rect(position);
            labelPos.y += _maxPropertyHeight;
            
            var height = style.CalcHeight(content, position.width) + 2.0f;
            labelPos.height = height;
            _maxPropertyHeight += height;
            
            EditorGUI.LabelField(labelPos, content, style);
            return labelPos;
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return _maxPropertyHeight + MaxPropertyHeight;
        }
    }
}