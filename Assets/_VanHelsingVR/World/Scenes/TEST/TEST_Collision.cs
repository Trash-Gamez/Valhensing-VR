using UnityEngine;

public class TEST_Collision : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision called from: " + name);
        Debug.Log("Collision other: " + other.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger called from: " + name);
        Debug.Log("Trigger other: " + other.gameObject.name);
    }

    [ContextMenu("")]
    public void SleepRB()
    {
        rb.Sleep();
    }
}
