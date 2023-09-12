using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaVida : MonoBehaviour
{
    private int _vida;
    public int Vida
    {
        get { return _vida; }
        set { _vida = value; }
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
