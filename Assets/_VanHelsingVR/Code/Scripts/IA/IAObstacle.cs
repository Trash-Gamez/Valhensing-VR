using System.Collections.Generic;
using RacTools.RuntimeSet;
using UnityEngine;

namespace _VanHelsingVR.IA
{
    public class IAObstacle : MonoBehaviour
    {
        [SerializeField] private List<RuntimeSet<IAObstacle>> runtimeSets;

        public float Radius => obstacleRadius;
        [SerializeField, Min( 0.1f)] private float obstacleRadius;
        public Vector3 Center => centerTransform.position;
        [SerializeField] private Transform centerTransform;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Center, obstacleRadius);
        }

        private void OnEnable()
        {
            foreach (var runtimeSet in runtimeSets)
            {
                runtimeSet.AddToSet(this);
            }
        }

        private void OnDisable()
        {
            foreach (var runtimeSet in runtimeSets)
            {
                runtimeSet.RemoveFromSet(this);
            }
        }
    }
}