using UnityEngine;

public class GunSelectionUI : MonoBehaviour
{
    [SerializeField] private Transform pointToFollow;

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = pointToFollow.position;
    }
}
