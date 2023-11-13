using RacTools.Variables;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private VariableReference<Transform> target;

    private void Update()
    {
        transform.position = target.Value.position; 
    }
}
