using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;

    [Range(0f, 1f)]
    public float positionDamping;
    [Range(0f, 1f)]
    public float rotationDamping;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;

        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationDamping);
    }
}
