
using System.Collections;
using _VanHelsingVR.Interaction;
using _VanHelsingVR.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using RacTools.Variables;
using TMPro;

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

#if UNITY_EDITOR
    [Title("Gun VFX")]
#endif

    [SerializeField] private ParticleSystemRenderer lighting;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private TextMeshPro magazineText;

    private bool _firstFrameDone = false;

    private void Start()
    {
        magazine.Value = 0;
        previousPos = transform.localPosition;
        
    }
    void Update()
    {
       
        
        GetInput();
       
        speedY = ((transform.localPosition.y - previousPos.y)) / Time.deltaTime;
        previousPos = transform.localPosition;
      
        if (Mathf.Abs(speedY) > speedLimit && canReload && grip)
        {
            StartCoroutine(nameof(ReloadCoroutine));
        }

        if (trigger && !grip)
        {
            StartCoroutine(Shoot());
        }

        Vfx();
    }
    
   private void Vfx()
    {
        float alpha = UtilitieExtensions.Map(magazine.Value, new Range(magazineSize, 0), Range.OneToZero);
        lighting.material.SetFloat("_Alpha", alpha);
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
        AudioManager.Instance.PlaySound3D("Reload", transform.position);
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
            AudioManager.Instance.PlaySound3D("FullReload", transform.position);
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
            if (Physics.Raycast(shootPoint.position, direction, out hit, fireRange, hittableLayer))
            {
                try
                {
                    hit.transform.GetComponent<Damagable>().OnDamage();
                    
                } catch(System.Exception)
                {
                    Debug.Log("This Object does not have Damagable Script");
                }
                if (hit.transform.CompareTag("Enemy"))
                {
                    AudioManager.Instance.PlaySound3D("FleshImpact_" + Random.Range(1, 10), hit.point);
                }
                else
                {
                    AudioManager.Instance.PlaySound3D("ConcreteImpact_0" + Random.Range(1, 8), hit.point);
                }
            }
            magazineText.color = Color.white;
            InstantiateVisual(direction);
            Debug.DrawRay(shootPoint.position, direction, Color.green);
            AudioManager.Instance.PlaySound3D("Shoot_0"+Random.Range(1,8), transform.position);
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
            AudioManager.Instance.PlaySound3D("DryShoot", transform.position);
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
        muzzle.Play();
    }
}
