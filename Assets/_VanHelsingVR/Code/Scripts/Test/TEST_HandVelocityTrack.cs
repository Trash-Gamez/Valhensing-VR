using System.Collections;
using System.Collections.Generic;
using _VanHelsingVR.Variables;
using UnityEngine;

public class TEST_HandVelocityTrack : MonoBehaviour
{
    [SerializeField]
    private FloatVariable _yPos;

    [SerializeField] 
    private FloatVariable _ySpeed;

    [SerializeField] 
    private FloatVariable _yDelta;
    
    [SerializeField]
    private bool _useLocal = false;

    IEnumerator Start()
    {
        while (true)
        {
            
            var newPos = transform.localPosition.y;

            _yDelta.Value = newPos - _yPos.Value;

            _ySpeed.Value = _yDelta.Value / Time.deltaTime;
        
            _yPos.Value = newPos;
            yield return new WaitForSeconds(0.0025f); 
        }
    }

}
