using RacTools.Variables;
using UnityEngine;

namespace _VanHelsingVR.Instructions
{
    public class LookAtInstruction : MonoBehaviour
    {
        [SerializeField] private VariableReference<Transform> lookAtTransform;
        //[SerializeField] private bool useLocalRotate;
        private void Update()
        {
            transform.LookAt(lookAtTransform.Value);
        }
    }
}
