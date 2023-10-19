using System;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public class BehaviourTreeController : MonoBehaviour
    {
        [SerializeField] private BehaviourTree behaviourTree;

        private void Update()
        {
            if (behaviourTree != null)
            {
                behaviourTree.Update();
            }
        }
    }
}