using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBigDoor : MonoBehaviour
{
    public Animator animator;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("Open", true);
            AudioManager.Instance.PlaySound2D("BigDoor");
        }
    }
}
