using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Gun : MonoBehaviour
{
    public TextMeshPro text;

    private Vector3 previousPos;
    private float speedY;
    private bool canReload=true;

    private int magazine=0;


    [SerializeField] private int magazineSize;
    [SerializeField] private float reloadTime;
    [SerializeField] private float speedLimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speedY = ((transform.position.y - previousPos.y)) / Time.deltaTime;
        previousPos = transform.position;
       // Debug.Log(speedY);
        if (Mathf.Abs(speedY) > speedLimit&&canReload)
        {
            StartCoroutine("ReloadCoroutine");
        }
    }

   IEnumerator ReloadCoroutine()
    {
        canReload = false;
        yield return new WaitForSeconds(reloadTime);
        Reload();
        canReload = true;
    }


    void Reload()
    {
        magazine++;
        if (magazine > magazineSize) magazine = magazineSize;
        text.text = magazine.ToString();
    }
}
