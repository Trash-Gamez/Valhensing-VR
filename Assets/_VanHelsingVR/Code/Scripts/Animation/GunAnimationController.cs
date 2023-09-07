using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GunAnimationController : MonoBehaviour
{

    [SerializeField] private InputActionProperty fistAnimationAction;
    [SerializeField] private InputActionProperty pointAnimationAction;

    [SerializeField] Animator handAnimator;
    void Update()
    {
        bool gripvalue = pointAnimationAction.action.ReadValue<bool>();
        bool triggervalue = fistAnimationAction.action.ReadValue<bool>();
        handAnimator.SetBool("Trigger", triggervalue);
        handAnimator.SetBool("Grip", gripvalue);
    }
}
