using System;
using UnityEngine;

namespace RacTools.BehaviourTree
{
    public class BehaviourTreeController : MonoBehaviour
    {
        [field: SerializeField] public BehaviourTree behaviourTree { get; private set; }

        private void Start()
        {
            if (behaviourTree == null) return;
            behaviourTree = behaviourTree.Clone();
            behaviourTree.Bind();
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