using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Hittable
{
    
    public override void OnHit()
    {
        transform.GetComponent<Renderer>().material.SetColor("_Color",Color.red);
    }


    IEnumerator ColorChange()
    {
        transform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.3f);
        transform.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }
}
