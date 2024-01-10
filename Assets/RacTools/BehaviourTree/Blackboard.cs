using RacTools.Variables;
using UnityEngine;
using UnityEngine.Serialization;

namespace RacTools.BehaviourTree
{
    [System.Serializable]
    public class Blackboard
    {
        [FormerlySerializedAs("playerPos")] public Variable<Transform> playerTransform;
        
    }
}
