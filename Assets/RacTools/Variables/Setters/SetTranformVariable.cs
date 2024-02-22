using RacTools.Variables;
using UnityEngine;

[DefaultExecutionOrder(-1)]

public class SetTranformVariable : MonoBehaviour
{
    [SerializeField] private Transform transformToSet;
    [SerializeField] private Variable<Transform> variable;
    private void Start()
    {
        variable.Value = transformToSet;
    }
}
