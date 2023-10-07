using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public float Time;
    public bool canContinue;
    public bool canRotate=true;

    public void Continue()
    {
        canContinue = true;
    }
}
