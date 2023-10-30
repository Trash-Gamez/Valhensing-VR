using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace RacTools.Views
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> {}

        private Editor _editor;
        
        public InspectorView()
        {
        
        }

        internal void OnSelectedObject(Object selectedObject)
        {
            Clear();
            
            Object.DestroyImmediate(_editor);
            _editor = Editor.CreateEditor(selectedObject);
            // This container is created because this clas inherits from VisualElement, so it needs IMGUIContainer to show up
            var container = new IMGUIContainer(() => _editor.OnInspectorGUI());
            Add(container);
        }
    }
}
