using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateIndication : MonoBehaviour
{
    public GameObject sign;
    [SerializeField] Animator anim;
    [SerializeField] string animName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sign.SetActive(true);
            if (anim == null) return;
            anim.Play(animName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sign.SetActive(false);
        }
    }
}
