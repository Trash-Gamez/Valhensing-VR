
using System;
using System.Collections;
using _VanHelsingVR.Health;
using _VanHelsingVR;
using _VanHelsingVR.Utilities;
using Autohand;
using UnityEngine;
using UnityEngine.InputSystem;
using RacTools.Variables;
using TMPro;

using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Range = RacTools.Utilities.Range;

public class Gun : MonoBehaviour
{
    #if UNITY_EDITOR
    [Title("Grabbable")] 
    #endif
    [SerializeField] private Grabbable gunGrabbable;
    
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
    [SerializeField] private GameObject fromGameObjectLayer;
    [SerializeField] private LayerMask hittableLayer;

    #if UNITY_EDITOR
    [Title("Gun Properties")]
#endif
    [SerializeField] private Rigidbody gunRigidbody;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private GameObject bloodEffect;
 
    
    #if UNITY_EDITOR
    [Title("Gun Input")]
    #endif
    [SerializeField] private InputActionProperty leftTriggerAction;
    [SerializeField] private InputActionProperty leftGripAction;
    [SerializeField] private InputActionProperty rightTriggerAction;
    [SerializeField] private InputActionProperty rightGripAction;

    private bool trigger;
    private bool grip;

    private float speedY;
    private bool canReload=true;
    private bool canShoot = true;

#if UNITY_EDITOR
    [Title("Gun VFX")]
#endif

    [SerializeField] private ParticleSystemRenderer lighting;
    //[SerializeField] private ParticleSystem muzzle;
    [SerializeField] private TextMeshPro magazineText;

    private Hand _holdingHand;
    private bool _isHoldingHand;
    
    private static readonly int _IsLoading = Animator.StringToHash("IsLoading");

    private void Start()
    {
        magazine.Value = 0;
    }
    
    void Update()
    {
        GetInput();
        gunAnimator.SetBool(_IsLoading, grip);
        speedY = gunRigidbody.angularVelocity.x + gunRigidbody.velocity.y; //Hacer queel angular sea más importante
        //Debug.Log("speed angula x: " + speedY);
      
        if (Mathf.Abs(speedY) > speedLimit && canReload && grip)
        {
            StartCoroutine(nameof(ReloadCoroutine));
        }

        if (trigger && !grip)
        {
            StartCoroutine(Shoot());
        }

        //Vfx();
    }
    
    private void Vfx()
    {
        float alpha = UtilitieExtensions.Map(magazine.Value, new Range(magazineSize, 0), Range.OneToZero);
        lighting.material.SetFloat("_Alpha", alpha);
    }

    private void GetInput()
    {
        if (!_isHoldingHand)
        {
            grip = false;
            trigger = false;
            return;
        }

        var isLeft = _holdingHand.left;
        var gripValue = isLeft ? leftGripAction.action.ReadValue<float>() : rightGripAction.action.ReadValue<float>() ;
        var triggerValue = isLeft ? leftTriggerAction.action.ReadValue<float>() : rightTriggerAction.action.ReadValue<float>();
        
        grip = gripValue > 0.3f;
        trigger = triggerValue > 0.3f;
        //trigger = _holdingHand ? _holdingHand.IsSqueezing(): false;
        //grip = _holdingHand ? _holdingHand.IsHolding(): false;
    }

   IEnumerator ReloadCoroutine()
    {
        canReload = false;
        gunAnimator.Play("Reload");
        if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("Reload");
        yield return new WaitForSeconds(reloadTime);
        Reload();
        canReload = true;
    }
   
    private void Reload()
    {
        magazine.Value += 5;
        magazineText.color = Color.white;
        if (magazine.Value >= magazineSize)
        {
            if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("FullReload");
            magazine.Value = magazineSize;
            magazineText.color = Color.green;
        }
    }

    IEnumerator Shoot()
    {
        if (!canShoot) yield break;
        
        if (magazine.Value > 0)
        {
            canShoot = false;
            RaycastHit hit;

            Vector3 direction = GetDirection();
            if (Physics.SphereCast(shootPoint.position, 0.25f, direction, out hit, fireRange, hittableLayer))
            {
                var hurtbox = hit.transform.GetComponent<Hurtbox>();
                if(hurtbox != null)
                {
                    hurtbox.OnHitScan(fromGameObjectLayer.layer);
                }
                else
                {
                    Debug.LogWarning("This Object does not have HurtBox Script");
                }                
            }
            
            magazineText.color = Color.white;
            InstantiateVisualNormal(direction);

            Debug.DrawRay(shootPoint.position, direction, Color.green);
            if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("Shoot_0"+Random.Range(1,8));
            
            magazine.Value--;
            if (magazine.Value <= 0)
            {
                magazine.Value = 0;
                magazineText.color = Color.red;
            }
            
            yield return new WaitForSeconds(shootingSpeed);
            canShoot = true;
        }
        else
        {
            canShoot = false;
            if(AudioManager.Instance) AudioManager.Instance.PlaySound3D("DryShoot", transform.position);
            
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

    private void InstantiateVisualNormal(Vector3 direction)
    {
        Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(direction),bulletParent);
        //muzzle.Play();
    }

    private void InstantiateVisualTele(Vector3 direction, Vector3 target)
    {
        Bullet actualBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(direction),bulletParent).GetComponent<Bullet>();
        //muzzle.Play();
        actualBullet.direction = (target - shootPoint.position).normalized;
    }

    #region Grabbable to Hand Section

    //Cuando alguien agarra la pistola
    private void OnGrab(Hand hand, Grabbable grabbable)
    {
        _holdingHand = hand;
        _isHoldingHand = true;
        Debug.Log("Grabbed");
    }

    //Cuando la mano libera a la pistola
    private void OnRelease(Hand hand, Grabbable grabbable)
    {
        _holdingHand = null;
        _isHoldingHand = false;
        Debug.Log("Released");
    }
    
    #endregion
    
    private void OnEnable()
    {
        leftTriggerAction.action.Enable();
        leftGripAction.action.Enable();
        rightTriggerAction.action.Enable();
        rightGripAction.action.Enable();
        
        gunGrabbable.OnGrabEvent += OnGrab;
        gunGrabbable.OnReleaseEvent += OnRelease;
    }

    private void OnDisable()
    {
        gunGrabbable.OnGrabEvent -= OnGrab;
        gunGrabbable.OnReleaseEvent -= OnRelease;
    }
}
