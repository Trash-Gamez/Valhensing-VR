using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class HandAnimationController : MonoBehaviour
{
    [SerializeField] private InputActionProperty fistAnimationAction;
    [SerializeField] private InputActionProperty pointAnimationAction;


    [SerializeField] Animator handAnimator;

    public GameObject fistCollider;

  

    void Update()
    {
        float gripvalue = pointAnimationAction.action.ReadValue<float>();
        float triggervalue = fistAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggervalue);
        handAnimator.SetFloat("Grip", gripvalue);

        if (triggervalue == 1)
        {
            fistCollider.SetActive(true);
        }
        else
        {
            fistCollider.SetActive(false);
        }
    }

    public void Grap(string nombre)
    {
        handAnimator.Play(nombre);
    }


}
