using System;
using UnityEngine;

public class CapsuleFollowing : MonoBehaviour
{
    [SerializeField] private CapsuleCollider thisCapsule;
    [SerializeField] private CapsuleCollider originalCapsule;

    private void Start()
    {
        thisCapsule.radius = originalCapsule.radius;
    }

    private void Update()
    {
        thisCapsule.transform.position = originalCapsule.transform.position;
    }

    private void FixedUpdate()
    {
        thisCapsule.center = originalCapsule.center;
        thisCapsule.height = originalCapsule.height;
    }
}
