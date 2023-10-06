using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class HandEffectsController : MonoBehaviour
{
    [SerializeField] private InputActionProperty fistAnimationAction;
    [SerializeField] private InputActionProperty pointAnimationAction;


    [SerializeField]private Renderer meshRenderer;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] Animator handAnimator;

    [SerializeField, Range(0.000001f, 1)] private float valueToCountFist;
    [SerializeField] private ConditionPool isGrabbingSomething;
    private float gripvalue, triggervalue;

    private MaterialPropertyBlock _mpb;
    private readonly int _alphaID = Shader.PropertyToID("_Alpha");

    private void Start()
    {
        _mpb = new MaterialPropertyBlock();
    }

    void Update()
    {
        gripvalue = pointAnimationAction.action.ReadValue<float>();
        triggervalue = fistAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggervalue);
        handAnimator.SetFloat("Grip", gripvalue);
        
        HandleParticles();
    }

    private void HandleParticles()
    {
        if (particleSystem == null) return;
        Debug.Log($"IsGrabbing: {isGrabbingSomething.CanDo}");
        bool particleEnable = !isGrabbingSomething && triggervalue > valueToCountFist;
        
        if (particleEnable && !particleSystem.isPlaying)
        {
            _mpb.SetFloat(_alphaID, 1);
            meshRenderer.SetPropertyBlock(_mpb);
            particleSystem.Play();
        }
        
        else if(particleSystem.isPlaying)
        {
            _mpb.SetFloat(_alphaID, 0);
            meshRenderer.SetPropertyBlock(_mpb);
            particleSystem.Stop();
        }
    }

    public void Grap(string nombre)
    {
        handAnimator.Play(nombre);
    }
}
