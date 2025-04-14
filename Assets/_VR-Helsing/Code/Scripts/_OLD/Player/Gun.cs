using System.Collections;
using _VanHelsingVR;
using _VanHelsingVR.Health;
using _VanHelsingVR.Utilities;
using Autohand;
using UnityEngine;
using UnityEngine.InputSystem;
using RacTools.Variables;
using TMPro;

using Sirenix.OdinInspector;
using Random = UnityEngine.Random;
using Range = RacTools.Utils.Range;

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
    [SerializeField]
    private GunData[] guns;
    [SerializeField] private GunData data;
    [SerializeField] private Renderer[] gunRenderers;
    [SerializeField] private float changeGunSpeedLimit;
    [SerializeField] private GameObject fromGameObjectLayer;
    [SerializeField] private LayerMask hittableLayer;
    private GunConfig _config;
    private int _currentGun = 0;

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

    private float speedY, speedZ;
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
        _config = data.Config;
    }
    
    void Update()
    {
        GetInput();
        gunAnimator.SetBool(_IsLoading, grip);
        speedY = gunRigidbody.angularVelocity.x + gunRigidbody.linearVelocity.y; //Hacer queel angular sea más importante
        speedZ = gunRigidbody.angularVelocity.z;
        //Debug. Log("speed angula x: " + speedY);
      
        if (Mathf.Abs(speedY) > _config.ReloadSpeedLimit && canReload && grip)
        {
            StartCoroutine(nameof(ReloadCoroutine));
        }
        
        if (Mathf.Abs(speedZ) > changeGunSpeedLimit && grip)
        {
            NextGun((int)Mathf.Sign(speedZ));
        }

        if (trigger && !grip)
        {
            StartCoroutine(Shoot());
        }

        //Vfx();
    }

    private void NextGun(int moveIndex)
    {
        _currentGun += moveIndex;
        if (_currentGun < 0) _currentGun = guns.Length - 1;
        if (_currentGun >= guns.Length) _currentGun = 0;
        
        ChangeGunData(guns[_currentGun]);
    }

    public void ChangeGunData(GunData newData)
    {
        if (newData.indexMesh < 0 || newData.indexMesh >= gunRenderers.Length) return;
        
        //StopAllCoroutines();
        data = newData;
        _config = data.Config;
        
        DisableAllRenderers();
        gunRenderers[data.indexMesh].enabled = true;
    }

    private void DisableAllRenderers()
    {
        for (int i = 0; i < gunRenderers.Length; i++)
        {
            gunRenderers[i].enabled = false;
        }
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
        yield return new WaitForSeconds(_config.ReloadTime);
        Reload();
        canReload = true;
    }
   
    private void Reload()
    {
        magazine.Value += 5;
        if(magazineText) magazineText.color = Color.white;
        if (magazine.Value >= _config.MagazineSize)
        {
            if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("FullReload");
            magazine.Value = _config.MagazineSize;
            if(magazineText) magazineText.color = Color.green;
        }
    }

    IEnumerator Shoot()
    {
        if (!canShoot) yield break;
        
        if (magazine.Value > 0)
        {
            gunRigidbody.AddForceAtPosition(-shootPoint.forward * (_config.RecoilForce * 0.1f), shootPoint.position);
            gunRigidbody.AddForceAtPosition(shootPoint.up * _config.RecoilForce, shootPoint.position);
            canShoot = false;
            RaycastHit hit;

            Vector3 direction = GetDirection();
            if (Physics.SphereCast(shootPoint.position, 0.25f, direction, out hit, _config.FireRange, hittableLayer))
            {
                var hurtbox = hit.transform.GetComponent<Hurtbox>();
                if(hurtbox != null)
                {
                    hurtbox.OnHitScan(fromGameObjectLayer.layer, _config.Damage);
                }
                else
                {
                    Debug.LogWarning("This Object does not have HurtBox Script");
                }                
            }
            
            if(magazineText) magazineText.color = Color.white;
            InstantiateVisualNormal(direction);

            Debug.DrawRay(shootPoint.position, direction, Color.green);
            if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("Shoot_0"+Random.Range(1,8));
            
            magazine.Value--;
            if (magazine.Value <= 0)
            {
                magazine.Value = 0;
                if(magazineText) magazineText.color = Color.red;
            }

            if (_config.SingleShot)
            {
                yield return new WaitUntil(() => !trigger);
            }
            else
            {
                yield return new WaitForSeconds(_config.ShootingSpeed);
            }
            canShoot = true;
        }
        else
        {
            canShoot = false;
            if(AudioManager.Instance) AudioManager.Instance.PlaySound3D("DryShoot", transform.position);
            
            if (_config.SingleShot)
            {
                yield return new WaitUntil(() => !trigger);
            }
            else
            {
                yield return new WaitForSeconds(_config.ShootingSpeed);
            }
            
            Debug.Log("Sin Munici�n");
            canShoot = true;
        }
        
    }
    
    private Vector3 GetDirection()
    {
        Vector3 newDirection = transform.forward;
        newDirection += new Vector3(UnityEngine.Random.Range(-_config.Spread, _config.Spread), UnityEngine.Random.Range(-_config.Spread, _config.Spread), UnityEngine.Random.Range(-_config.Spread, _config.Spread));
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
