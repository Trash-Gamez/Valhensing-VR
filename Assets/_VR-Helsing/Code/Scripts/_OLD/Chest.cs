using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject SpawnObject;
    private Animator anim;
    [SerializeField] Vector3 spawnOffset;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void OpenChest()
    {
        anim.Play("Open");
        Instantiate(SpawnObject, this.transform.position+spawnOffset, transform.parent.rotation);
    }
}
