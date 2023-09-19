using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeftHandGrabber : Grabber
{
    #region Input

    [SerializeField] private InputActionProperty grabInput;
    [SerializeField, Range(0.001f,1f)] private float minimumValueToGrab;
    
    #endregion
    
    [SerializeField] private float grabRadius;
    [SerializeField] private LayerMask grabMask;
    
    private RaycastHit[] grabHits = new RaycastHit[5];
    private int _hitNumber = 0;
    private bool _alreadyCheckedHits = false;
    
    protected override void TryGrab(Grabbable grabbable)
    {
        if(canGrab)
            base.TryGrab(grabbable);
    }

    protected override void UnGrab(Grabbable grabbable)
    {
        base.UnGrab(grabbable);
    }

    private void Update()
    {
        if (_alreadyCheckedHits) return;

        var grabValue = grabInput.action.ReadValue<float>();
        if (grabValue < minimumValueToGrab) return;
        if (_hitNumber <= 0) return;
        
        //TRY TO GRAB OBJECT
        Grabbable nearestGrabbable = null;
        
        //Iterate over the hits of the sphere
        for (int i = 0; i < _hitNumber; i++)
        {
            var hit = grabHits[i];
            //Know if the hits contain the grabable component
            if (!hit.transform.TryGetComponent(out Grabbable newGrabable))
            {
                Debug.LogWarning($"El objeto: {hit.collider.name} no tiene componente 'Grabbable'");
                continue;
            }
            
            //if in this iteration nearestGrabbable is null, just assign the nearest as this object an continue the loop
            if (nearestGrabbable == null)
            {
                nearestGrabbable = newGrabable;
                continue;
            }
            //if nearest DOES exists
            //Compare distances between the new grabbable and the nearestGrabbable 
            var nearestDistance = Vector3.Distance(grabOrigin.position, nearestGrabbable.transform.position);
            var newdistance = Vector3.Distance(grabOrigin.position, newGrabable.transform.position);
            
            //if newGrabbale is nearest than the last nearestGrabbale, change it's value
            if (nearestDistance > newdistance) nearestGrabbable = newGrabable;
        }

        if (nearestGrabbable == null)
        {
            _alreadyCheckedHits = true;
            return;
        }

        TryGrab(nearestGrabbable);
    }

    private void FixedUpdate()
    {
        _hitNumber = Physics.SphereCastNonAlloc(base.grabOrigin.position, grabRadius, transform.right, grabHits,grabMask);
        _alreadyCheckedHits = false;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = _hitNumber > 0 ? Color.green : Color.red;
        Gizmos.DrawSphere(grabOrigin.position, grabRadius);
    }
}
