using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
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
    [SerializeField] private Animator gunAnimator;
    

    [SerializeField] private InputActionProperty fistAnimationAction;
    [SerializeField] private InputActionProperty pointAnimationAction;

    private bool trigger;
    private bool grip;
    void Update()
    {
        GetInput();
       
        speedY = ((transform.position.y - previousPos.y)) / Time.deltaTime;
        previousPos = transform.position;
      
        if (Mathf.Abs(speedY) > speedLimit&&canReload&&!grip)
        {
            StartCoroutine("ReloadCoroutine");
        }
    }

    void GetInput()
    {
        float gripvalue = pointAnimationAction.action.ReadValue<float>();
        float triggervalue = fistAnimationAction.action.ReadValue<float>();
        if (gripvalue != 0) { grip = true; } else { grip = false; }
        if (triggervalue != 0) { trigger = true; } else { trigger = false; }

    }

   IEnumerator ReloadCoroutine()
    {
        canReload = false;
        gunAnimator.Play("Reload");
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
