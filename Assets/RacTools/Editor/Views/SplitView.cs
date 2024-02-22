using UnityEngine.UIElements;

namespace RacTools.Editor.Views
{
    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<SplitView, TwoPaneSplitView.UxmlTraits> {}

        public SplitView()
        {
            
        }
    }
}
