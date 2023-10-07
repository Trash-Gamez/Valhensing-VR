using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool canMove = true;

    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed=0.01f;

    [SerializeField] private WayPoint[] wayPoints;
    private int actualIndex = 0;

    public void Start()
    {

        StartCoroutine(MoveToNextPoint(wayPoints[actualIndex].transform.position));
    }

    IEnumerator MoveToNextPoint(Vector3 nextPosition)
    {
        while (canMove)
        {
            if (wayPoints[actualIndex].canRotate)
            {
                RotateToNextPoint(nextPosition);
            }
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

            if (transform.position == nextPosition)
            {

                canMove = false;

            }

            yield return null;
        }
        yield return new WaitForSeconds(wayPoints[actualIndex].Time);

        NextPoint(wayPoints[actualIndex].canContinue);
    }

    void RotateToNextPoint(Vector3 nextPoint)
    {
       
        Vector3 targetDirection = nextPoint - transform.position;

       
        float singleStep = rotationSpeed * Time.deltaTime;

        
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        
        Debug.DrawRay(transform.position, newDirection, Color.red);

        
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void NextPoint(bool canContinue)
    {
        actualIndex++;
        Debug.Log("Next Index: " + actualIndex);
        if (!canContinue) return;
        canMove = true;
        StartCoroutine("MoveToNextPoint", wayPoints[actualIndex].transform.position);
    }

    public void RestartMovement()
    {
        canMove = true;
        StartCoroutine("MoveToNextPoint", wayPoints[actualIndex].transform.position);
    }

    
       
    
}
