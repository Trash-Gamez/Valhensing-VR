using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHand : MonoBehaviour
{
    private HandEffectsController _handEffectsController;
    public string nombreAnimacion;
    
    void Start()
    {
        _handEffectsController = FindObjectOfType<HandEffectsController>();
    }

    public void LlamarAnimacion()
    {
        _handEffectsController.Grap(nombreAnimacion);
    }

    
}
