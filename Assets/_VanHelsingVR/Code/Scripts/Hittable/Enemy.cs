using System;
using System.Collections;
using UnityEngine;

public class Enemy : Hittable
{
    [SerializeField] Renderer[] renderers;

    private Coroutine _hitCoroutine;

    private MaterialPropertyBlock _materialRed;
    private MaterialPropertyBlock MaterialRed
    {
        get
        {
            if (_materialRed == null)
                _materialRed = new();
            return _materialRed;
        }
    }
    
    private MaterialPropertyBlock _materialWhite;
    private MaterialPropertyBlock MaterialWhite
    {
        get
        {
            if (_materialWhite == null)
                _materialWhite = new();
            return _materialWhite;
        }
    }

    private void Start()
    {
        MaterialRed.SetColor("_Color", Color.red);
        MaterialWhite.SetColor("_Color", Color.white);
    }

    public override void OnHit()
    {
        if (_hitCoroutine != null) return;
        _hitCoroutine = StartCoroutine("ColorChange");
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

        _hitCoroutine = null;
    }

   
}
