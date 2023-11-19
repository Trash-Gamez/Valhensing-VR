using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeDecals : MonoBehaviour
{
    public ParticleSystem ps;
    public GameObject Decal;
    public List<ParticleCollisionEvent> collisionEvents;
    private static Queue<GameObject> _decalQueue = new Queue<GameObject>();
    private static int _maxAmount = 100;
    
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
            _decalQueue.Enqueue(actualObject);
            if (_decalQueue.Count >= _maxAmount)
            {
                var decal = _decalQueue.Dequeue();
                Destroy(decal);
            }

            actualObject.transform.Rotate(Vector3.forward*(Random.Range(-180,180)));
            Debug.Log(collisionEvents[i].normal);
            i++;
        }
       
    }
}
