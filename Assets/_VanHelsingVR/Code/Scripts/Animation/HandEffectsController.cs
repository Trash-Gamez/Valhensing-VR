using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class HandEffectsController : MonoBehaviour
{
    [Title("Input")][SerializeField]
    private HandInputReference handInput;

    [Title("Effects Reference")]
    [SerializeField]private Renderer meshRenderer;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] Animator handAnimator;
    
    [Space, SerializeField] private ConditionPool canActiveEffects;
    
    private MaterialPropertyBlock _mpb;
    private readonly int _alphaID = Shader.PropertyToID("_Alpha");
    
    private bool _isInPunchPose;
    
    private void Start()
    {
        _mpb = new MaterialPropertyBlock();
    }

    void Update()
    {
        HandleAnimations();
        HandleParticles();
    }
    
    private void HandleAnimations()
    {
        handAnimator.SetFloat("Trigger", handInput.Reference.ActiveInput);
        handAnimator.SetFloat("Grip", handInput.Reference.SelectionInput);
    }

    private bool _areEffectsActive = true;
    private void HandleParticles()
    {
        if (particleSystem == null) return;
        Debug.Log($"Can Active Effects: {canActiveEffects.CanDo}");
        canActiveEffects.LogVars();
        
        if (canActiveEffects && !_areEffectsActive)
        {
            _mpb.SetFloat(_alphaID, 1);
            meshRenderer.SetPropertyBlock(_mpb);
            particleSystem.Play();
        }
        else if(!canActiveEffects && _areEffectsActive)
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
