using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeDecals : MonoBehaviour
{
    public ParticleSystem ps;
    public GameObject Decal;
    public DecalManager decalManager;
    public List<ParticleCollisionEvent> collisionEvents;
    private void Start()
    {
        Destroy(gameObject, 3);
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);
        int i = 0;

        while (i < numCollisionEvents)
        {
            GameObject actualObject = Instantiate(Decal, collisionEvents[i].intersection, Quaternion.LookRotation(-collisionEvents[i].normal),other.transform);
            
            actualObject.transform.Rotate(Vector3.forward*(Random.Range(-180,180)));
            Debug.Log(collisionEvents[i].normal);
            i++;
        }
       
    }
}
