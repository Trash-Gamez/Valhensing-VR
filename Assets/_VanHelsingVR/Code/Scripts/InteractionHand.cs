using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHand : MonoBehaviour
{
    private HandAnimationController handAnimationController;
    public string nombreAnimacion;
    
    void Start()
    {
        handAnimationController = FindObjectOfType<HandAnimationController>();
    }

    public void LlamarAnimacion()
    {
        handAnimationController.Grap(nombreAnimacion);
    }

    
}
