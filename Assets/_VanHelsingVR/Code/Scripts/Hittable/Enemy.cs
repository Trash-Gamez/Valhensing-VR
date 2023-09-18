using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Hittable
{
    [SerializeField] Renderer[] renderers;
    private MaterialPropertyBlock _materialRed = new();
    private MaterialPropertyBlock _materialWhite = new();

    private void Start()
    {
        _materialRed.SetColor("_Color", Color.red);
        _materialWhite.SetColor("_Color", Color.white);
    }

    public override void OnHit()
    {
        StartCoroutine("ColorChange");
    }

    IEnumerator ColorChange()
    {
        
        foreach (Renderer renderer in renderers)
        {
            renderer.SetPropertyBlock(_materialRed);
        }
        yield return new WaitForSeconds(0.3f);
        foreach (Renderer renderer in renderers)
        {
            renderer.SetPropertyBlock(_materialWhite);
        }
    }

   
}
