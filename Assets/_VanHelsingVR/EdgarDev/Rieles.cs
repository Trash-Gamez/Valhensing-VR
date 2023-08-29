using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rieles : MonoBehaviour
{
    public List<Transform> wayPoints = new List<Transform>();
    public float velocidad;
    int currentIndex = 0;
    private Coroutine corWaypoint;

    void Start()
    {
        if(corWaypoint != null)
        {
            StopCoroutine(corWaypoint);
        }
        corWaypoint = StartCoroutine(CorWaypoints());
    }

    void Update()
    {
        Debug.Log(velocidad);
    }

    IEnumerator CorWaypoints()
    {
       
        
        float dis = Vector3.Distance(transform.position, wayPoints[currentIndex].transform.position);
        Debug.Log(dis);
        while (dis > 1)
        {
            dis = Vector3.Distance(transform.position, wayPoints[currentIndex].transform.position);

            gameObject.transform.Translate((
                wayPoints[currentIndex].transform.position - transform.position) 
                * velocidad * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        currentIndex++;
        //checar si existe un siguiente, y volver a mandar la corutina
        if (wayPoints[currentIndex].transform != null)
        {
            StartCoroutine(CorWaypoints());
        }
        else
        {
            StopCoroutine(CorWaypoints());
        }
    }
}
