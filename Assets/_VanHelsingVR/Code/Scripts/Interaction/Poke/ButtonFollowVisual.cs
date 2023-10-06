using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class ButtonFollowVisual : MonoBehaviour
{

    public Transform visualTarget;
    public Vector3 localAxis;
    private Vector3 _initialLocalPos;
    public float resetSpeed = 5;
    public float followAngle = 45;

    private bool freeze = false;

    private Vector3 _offset;
    private Transform pokeAttachTransform;

    private XRBaseInteractable _interactable;
    private bool isFollowing = false;
    // Start is called before the first frame update
    void Start()
    {
        _initialLocalPos = visualTarget.localPosition;

        _interactable = GetComponent<XRBaseInteractable>();
        _interactable.hoverEntered.AddListener(Follow);
        _interactable.hoverExited.AddListener(Reset);
        _interactable.selectEntered.AddListener(Freeze);
    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;
            
            pokeAttachTransform = interactor.attachTransform;
            _offset = visualTarget.position - pokeAttachTransform.position;

            float pokeAngle = Vector3.Angle(_offset, visualTarget.TransformDirection(localAxis));
            if (pokeAngle < followAngle)
            {
                isFollowing = true;
                freeze = false;
            }
        }
    }

    public void Reset(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            isFollowing = false;
            freeze = false;
        }
    }

    public void Freeze(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            freeze = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze) return;
        if (isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + _offset);
            Vector3 constrainedlocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);

            visualTarget.position = visualTarget.TransformPoint(constrainedlocalTargetPosition);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition,_initialLocalPos,Time.deltaTime*resetSpeed);
        }
    }
}
