using UnityEngine;

public class TEST_ConditionPool : MonoBehaviour
{
    [SerializeField] private ConditionPool pool;

    private void Update()
    {
        string message = pool ? "Se puede hacer todo" : "No se puede hacer nada";
        Debug.Log(message);
    }
}
