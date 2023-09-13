using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class HandAnimationController : MonoBehaviour
{
    [SerializeField] private InputActionProperty fistAnimationAction;
    [SerializeField] private InputActionProperty pointAnimationAction;

    [SerializeField] Animator handAnimator;

    void Update()
    {
        float gripvalue = pointAnimationAction.action.ReadValue<float>();
        float triggervalue = fistAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggervalue);
        handAnimator.SetFloat("Grip", gripvalue);
    }
}
