using System.Collections;
using System.Collections.Generic;
using _VanHelsingVR.Variables;
using UnityEngine;

public class SistemaVida : MonoBehaviour
{
    private Variable<int> _vida;
    public int Vida
    {
        get { return _vida.Value; }
        set { _vida.Value = value; }
    }
    SistemaVida sistemaVida = new SistemaVida();

    public void Damage(int hit)
    {
        sistemaVida.Vida-=hit;
 
    }

    public void Health(int life)
    {
        sistemaVida.Vida+=life;
    }

    
    
}
