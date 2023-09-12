using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Hittable
{
    [SerializeField] Renderer[] renderers;
    public override void OnHit()
    {
        StartCoroutine("ColorChange");
    }


    IEnumerator ColorChange()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.SetColor("_Color", Color.red);
        }
        yield return new WaitForSeconds(0.3f);
        foreach (Renderer renderer in renderers)
        {
            renderer.material.SetColor("_Color", Color.white);
        }
    }

   
}
