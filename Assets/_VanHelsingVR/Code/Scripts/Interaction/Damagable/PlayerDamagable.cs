using System.Collections;
using System.Collections.Generic;
using _VanHelsingVR.Interaction;
using UnityEngine;

public class PlayerDamagable : Damagable
{
    public override void OnDamage()
    {
        //Animacion del golpe
        Debug.Log("Player Has Been Hit");
        base.OnDamage();
    }
}
