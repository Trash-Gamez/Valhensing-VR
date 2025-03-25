using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffects : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.CompareTag("Enemy"))
        {
            AudioManager.Instance.PlaySound3D("FleshImpact_" + Random.Range(1, 10),collision.transform.position);
        }
        else
        {
            AudioManager.Instance.PlaySound3D("ConcreteImpact_0" + Random.Range(1, 8), collision.transform.position);
            Debug.Log("AudioConcreto");
        }

        Destroy(gameObject);
    }
}
