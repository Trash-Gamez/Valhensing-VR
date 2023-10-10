using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalManager : MonoBehaviour
{
    
    [SerializeField] private float decalLife;
    [SerializeField] private float fadeOutTime;

    private void Start()
    {
       

    }

    public IEnumerator DecalCoroutine(GameObject decal)
    {
        DecalProjector projector = decal.GetComponent<DecalProjector>();


        yield return new WaitForSeconds(decalLife);
       
        for(int i = 0; i<=20;i++) {
            projector.fadeFactor -= .05f;
           
            yield return new WaitForSeconds(fadeOutTime);
        }
        yield return new WaitForSeconds(fadeOutTime * 10);
        Destroy(decal);
    }
}
