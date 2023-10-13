using _VanHelsingVR.Conditions;
using UnityEngine;

namespace _VanHelsingVR.TESTS
{
    public class TEST_ConditionPool : MonoBehaviour
    {
        [SerializeField] private ConditionPool pool;

        private void Update()
        {
            string message = pool ? "Se puede hacer todo" : "No se puede hacer nada";
            UnityEngine.Debug.Log(message);
        }
    }
}
