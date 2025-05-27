using System.Collections;
using Autohand;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZiplineController : MonoBehaviour
{
    [SerializeField] private Grabbable ziplineGrabbable;
    [SerializeField] private InputActionProperty leftHoldButton, rightHoldButton;
    [SerializeField] private Transform handleZipline;

    [Header("Zipline Points")]
    [SerializeField] private Transform[] ziplinePoints;

    [Header("Extra")]
    [SerializeField] private float ziplineSpeed;
    [SerializeField] private bool canDetach;

    private int _currentPointIndex;
    private Transform CurrentPoint => ziplinePoints[_currentPointIndex];

    private Hand _hand;

    private Coroutine _currentZiplineCoroutine;

    private bool IsHandHolding()
    {
        return _hand.left
            ? leftHoldButton.action.IsPressed()
            : rightHoldButton.action.IsPressed();
    }

    private void GetNewPoint()
    {
        if (++_currentPointIndex >= ziplinePoints.Length)
        {
            _currentPointIndex = 0;
        }
    }

    private IEnumerator ZiplineMoveCoroutine()
    {
        var point = CurrentPoint;

        while (Vector3.Distance(handleZipline.position, point.position) > 0.001f)
        {
            handleZipline.position = Vector3.MoveTowards(handleZipline.position, point.position, ziplineSpeed * Time.deltaTime);
            yield return null;
        }

        handleZipline.position = point.position;


        if (IsHandHolding())
            yield return new WaitWhile(IsHandHolding);


        _hand = null;
    }

    private IEnumerator ZiplineDetachCouroutine()
    {
        var point = CurrentPoint;

        while (IsHandHolding() || Vector3.Distance(handleZipline.position, point.position) > Mathf.Epsilon)
        {
            handleZipline.position = Vector3.MoveTowards(handleZipline.position, point.position, ziplineSpeed * Time.deltaTime);
            yield return null;
        }

        if (IsHandHolding())
            yield return new WaitWhile(IsHandHolding);


        _hand = null;
    }

    private void OnGrab(Hand hand, Grabbable grabbable)
    {
        _hand = hand;
        GetNewPoint();

        if (canDetach)
            _currentZiplineCoroutine = StartCoroutine(ZiplineDetachCouroutine());
        else
            _currentZiplineCoroutine = StartCoroutine(ZiplineMoveCoroutine());
    }

    private void OnEnable()
    {
        ziplineGrabbable.OnGrabEvent += OnGrab;
        ziplineGrabbable.OnReleaseEvent += ResetZipline;
    }


    private void OnDisable()
    {
        ziplineGrabbable.OnReleaseEvent -= ResetZipline;
        ziplineGrabbable.OnGrabEvent -= OnGrab;
    }

    public IEnumerator ResetZiplineCoroutine()
    {
        while (Vector3.Distance(handleZipline.position, ziplinePoints[0].position) > 0.001f)
        {
            handleZipline.position = Vector3.MoveTowards(handleZipline.position, ziplinePoints[0].position, ziplineSpeed * Time.deltaTime);
            yield return null;
        }

        handleZipline.position = ziplinePoints[0].position;
    }


    public void ResetZipline(Hand hand, Grabbable grabbable)
    {
        if (_currentZiplineCoroutine != null)
        {
            StopCoroutine(_currentZiplineCoroutine);
            _currentZiplineCoroutine = null;
        }

        _hand = hand;
        Debug.Log("Resetting zipline...");
        StartCoroutine(ResetZiplineCoroutine());
    }
}
