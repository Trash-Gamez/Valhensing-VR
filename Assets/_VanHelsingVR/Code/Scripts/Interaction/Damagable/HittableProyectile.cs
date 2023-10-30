using System;
using _VanHelsingVR.Interaction;
using UnityEngine;


public class HittableProyectile : Hittable
{
    public Transform target;
    [SerializeField] private float speed;
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        var dir = target.position - transform.position;
        dir.Normalize();
        var rot = Quaternion.LookRotation(dir);
        transform.rotation = rot;
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
