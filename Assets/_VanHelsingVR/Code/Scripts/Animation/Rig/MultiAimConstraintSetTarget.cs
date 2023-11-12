using System;
using RacTools.Variables;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace _VanHelsingVR.Animation.Rig
{
    [RequireComponent(typeof(MultiAimConstraint))]
    public class MultiAimConstraintSetTarget : MonoBehaviour
    {
        [SerializeField]
        private VariableReference<Transform> target;

        [SerializeField] private VariableReference<float> weight;
        private void Awake()
        {
            var multiAimContraint = GetComponent<MultiAimConstraint>();
            
            weight.Value = Mathf.Clamp01(weight.Value);
            var data = multiAimContraint.data;
            data.sourceObjects.Add(new WeightedTransform(target.Value, weight.Value));
            multiAimContraint.data = data;
        }
    }
}
