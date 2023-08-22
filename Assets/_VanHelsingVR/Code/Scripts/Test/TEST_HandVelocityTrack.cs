using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_HandVelocityTrack : MonoBehaviour
{
    [SerializeField]
    private FloatVariable _ySpeed;

    [SerializeField]
    private bool _useLocal = false;

    private void Update()
    {
        //Log Y pos
        _ySpeed.Value = transform.localPosition.y;
    }

}
