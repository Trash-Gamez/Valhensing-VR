using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool canMove = true;

    [SerializeField] private float speed;

    [SerializeField] private WayPoint[] wayPoints;
    private int actualIndex=0;
   
    public void Start()
    {
       
        StartCoroutine(MoveToNextPoint(wayPoints[actualIndex].transform.position));
    }

    IEnumerator MoveToNextPoint(Vector3 nextPosition)
    {
        while (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed*Time.deltaTime);

            if (transform.position == nextPosition) 
            { 
                
                canMove = false;
               
            }

            yield return null;
        }
        yield return new WaitForSeconds(wayPoints[actualIndex].Time);
       
        NextPoint(wayPoints[actualIndex].canContinue);
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
