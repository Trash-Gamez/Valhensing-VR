using UnityEngine;

namespace RacTools.BehaviourTree
{
    [CreateAssetMenu(fileName = "BehaviourTree", menuName = "RacTools/BehaviourTree")]
    public class BehaviourTree : ScriptableObject
    {
        public Node root;
        public Node.State treeState = Node.State.Running;

        public Node.State Update()
        {
            if (treeState == Node.State.Running)
            {
                treeState = root.Update();
            }

            return treeState;
        }
    }
}