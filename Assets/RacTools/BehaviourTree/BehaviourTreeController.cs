using System;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public class BehaviourTreeController : MonoBehaviour
    {
        [SerializeField] private BehaviourTree behaviourTree;

        private void Start()
        {
            if (behaviourTree == null) return;
            behaviourTree = behaviourTree.Clone();
        }

        private void Update()
        {
            if (behaviourTree != null)
            {
                behaviourTree.Update();
            }
        }
    }
}