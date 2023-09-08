using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System;
public class Gun : MonoBehaviour
{
    public TextMeshPro text;

    private Vector3 previousPos;
    private float speedY;
    private bool canReload=true;
    private bool canShoot = true;

    private int magazine=0;

    [Header("Gun Settings")]
    [SerializeField] private int magazineSize;
    [SerializeField] private float reloadTime;
    [SerializeField] private float speedLimit;
    [SerializeField] private float shootingSpeed;
    [SerializeField] private float spread;
    [SerializeField] private float fireRange;
    [SerializeField] private LayerMask hittableLayer;

    [Header("Gun Properties")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private InputActionProperty gripAction;

    private bool trigger;
    private bool grip;


    void Update()
    {
        text.text = magazine.ToString();
        GetInput();
       
        speedY = ((transform.position.y - previousPos.y)) / Time.deltaTime;
        previousPos = transform.position;
      
        if (Mathf.Abs(speedY) > speedLimit&&canReload&&!grip)
        {
            StartCoroutine("ReloadCoroutine");
        }

        if (trigger)
        {
            StartCoroutine("Shoot");
        }
    }

    private void GetInput()
    {
        float gripvalue = gripAction.action.ReadValue<float>();
        float triggervalue = triggerAction.action.ReadValue<float>();
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


    private void Reload()
    {
        magazine+=2;
        if (magazine > magazineSize) magazine = magazineSize;
       
       
    }

    IEnumerator Shoot()
    {
        if (canShoot)
        {
            if (magazine > 0)
            {
                canShoot = false;
                RaycastHit hit;




                Vector3 direction = GetDirection();
                if (Physics.Raycast(shootPoint.position, direction, out hit, fireRange, hittableLayer))
                {
                    Debug.Log(hit.transform.name);
                    Debug.Log("Shoot");
                    try
                    {
                        hit.transform.GetComponent<Hittable>().OnHit();
                    } catch(Exception e)
                    {
                        Debug.Log("This Object does not have Hittable Script");
                    }
                }
                InstantiateVisual(direction);
                Debug.DrawRay(shootPoint.position, direction, Color.green);
                magazine--;
                if (magazine < 0) magazine = 0;
                yield return new WaitForSeconds(shootingSpeed);
                canShoot = true;
            }
            else
            {
                canShoot = false;
                yield return new WaitForSeconds(shootingSpeed);
                Debug.Log("Sin Munición");
                canShoot = true;
            }
        }

       
    }


    private Vector3 GetDirection()
    {
        Vector3 newDirection = transform.forward;
        newDirection += new Vector3(UnityEngine.Random.Range(-spread, spread), UnityEngine.Random.Range(-spread, spread), UnityEngine.Random.Range(-spread, spread));
        newDirection.Normalize();
        return newDirection;
    }

    private void InstantiateVisual(Vector3 direction)
    {
        Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(direction));
    }
}
