using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBigDoor : MonoBehaviour
{
    public Animator animator;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("Open", false);
            animator.SetBool("True", true);
        }
    }
}
