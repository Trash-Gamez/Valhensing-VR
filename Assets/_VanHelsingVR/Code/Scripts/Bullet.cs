using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        Destroy(gameObject, 10);
    }
    void Update()
    {
        gameObject.transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
    }
}
