using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagable : Damagable
{
    public override void OnHit()
    {
        //Animacion del golpe
        Debug.Log("Player Has Been Hit");
        base.OnHit();
    }
}
