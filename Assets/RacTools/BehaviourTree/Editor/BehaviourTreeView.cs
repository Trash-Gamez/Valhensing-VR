using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace RacTools.BehaviourTree.Editor
{
    public class BehaviourTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> {}
        public BehaviourTreeView()
        {
            
        }
    }
}