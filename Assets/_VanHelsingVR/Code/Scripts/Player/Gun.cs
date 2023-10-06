using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using _VanHelsingVR.Variables;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

public class Gun : MonoBehaviour
{
    #if UNITY_EDITOR
    [Title("Gun Variables")]
    #endif
    [SerializeField]
    private Variable<int> magazine;

    #if UNITY_EDITOR
    [Title("Gun Settings")]
    #endif
    [SerializeField] private int magazineSize;
    [SerializeField] private float reloadTime;
    [SerializeField] private float speedLimit;
    [SerializeField] private float shootingSpeed;
    [SerializeField] private float spread;
    [SerializeField] private float fireRange;
    [SerializeField] private LayerMask hittableLayer;

    #if UNITY_EDITOR
    [Title("Gun Properties")]
    #endif
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;

    #if UNITY_EDITOR
    [Title("Gun Input")]
    #endif
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private InputActionProperty gripAction;

    private bool trigger;
    private bool grip;

    private Vector3 previousPos;
    private float speedY;
    private bool canReload=true;
    private bool canShoot = true;
    

    void Update()
    {
        GetInput();
       
        speedY = ((transform.position.y - previousPos.y)) / Time.deltaTime;
        previousPos = transform.position;
      
        if (Mathf.Abs(speedY) > speedLimit && canReload && !grip)
        {
            StartCoroutine(nameof(ReloadCoroutine));
        }

        if (trigger && grip)
        {
            StartCoroutine(nameof(Shoot));
        }
    }

    private void GetInput()
    {
        var gripValue = gripAction.action.ReadValue<float>();
        var triggerValue = triggerAction.action.ReadValue<float>();

        grip = gripValue > 0;
        trigger = triggerValue > 0;
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
        magazine.Value += 2;
        if (magazine.Value > magazineSize) magazine.Value = magazineSize;
    }

    IEnumerator Shoot()
    {
        if (!canShoot) yield break;
        
        if (magazine.Value > 0)
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
                    hit.transform.GetComponent<Hittable>().OnDamage();
                } catch(Exception)
                {
                    Debug.Log("This Object does not have Hittable Script");
                }
            }
            InstantiateVisual(direction);
            Debug.DrawRay(shootPoint.position, direction, Color.green);
            magazine.Value--;
            if (magazine.Value < 0) magazine.Value = 0;
            yield return new WaitForSeconds(shootingSpeed);
            canShoot = true;
        }
        else
        {
            canShoot = false;
            yield return new WaitForSeconds(shootingSpeed);
            Debug.Log("Sin Munici�n");
            canShoot = true;
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
