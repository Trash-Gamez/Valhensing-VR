using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float timeOfWait = 1f;
    public float radioExplotion = 8f;
    public Transform t;

    // Start is called before the first frame update
    void Start()
    {
        t.localScale = Vector3.zero;
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        t.localScale = new Vector3(radioExplotion,radioExplotion,radioExplotion);
        yield return new WaitForSeconds(timeOfWait);
        t.localScale = Vector3.zero;
    }



}
