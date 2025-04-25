using System.Collections;
using System.Linq;
using Autohand;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

using _VanHelsingVR;
using _VanHelsingVR.Animation.Gun;
using _VanHelsingVR.Health;
using RacTools.Variables;

using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    private const int MOVE_BUFFER_SIZE = 4;
    private readonly int[] _magazinesLoad = new int[3];
    
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
    [SerializeField] private float secondsToChangeWeapon;
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
    [SerializeField] private GunInput gunInput;
    [SerializeField] private InputActionProperty leftTriggerAction;
    [SerializeField] private InputActionProperty leftGripAction;
    [SerializeField] private InputActionProperty rightTriggerAction;
    [SerializeField] private InputActionProperty rightGripAction;

    private bool trigger;
    private bool grip;

    //private float speedY, speedZ;
    private int _bufferIterator = -1;
    private readonly float[] _xAxisBuffer = new float[MOVE_BUFFER_SIZE];
    private readonly float[] _zAxisBuffer = new float[MOVE_BUFFER_SIZE];
    
    private bool canReload=true;
    private bool canShoot = true;

#if UNITY_EDITOR
    [Title("Gun VFX")]
#endif
    [SerializeField] private ParticleSystemRenderer lighting;
    [SerializeField] private TextMeshPro magazineText;
    

    private Hand _holdingHand;
    private bool _isHoldingHand;
    
    private static readonly int _IsLoading = Animator.StringToHash("IsLoading");

    private void Start()
    {
        magazine.Value = 0;
        _config = data.Config;
    }

    private void FixedUpdate()
    {
        GetLocalAngularVelocities();
    }

    private void GetLocalAngularVelocities()
    {
        if (!grip) return;
        var angularGlobalGunVelocity = gunRigidbody.angularVelocity; //Velocidad angular GLOBAL (esto ultimo no lo sabia xd)
        /* PROYECTAMOS (mucho ojo que no sabia que el dot era proyeccion)
         * Proyectamos, la velocidad angular del mundo, SOBRE la direccion local del eje
         * Esto nos dará como resultado la velocidad angular local en x (es la que buscamos para la animacion de rotar)
         */

        //                                        Velocidad angular global       eje local de el arma en x      Conv a grados
        //                                                  |                            |                           |
        var angularXVelocity = Vector3.Dot(angularGlobalGunVelocity, gunRigidbody.transform.right) * Mathf.Rad2Deg;

        //Hacemos lo mismo pero en el eje Z que nos dará la velocidad local para el cambio de arma
        
        var angularZVelocity = Vector3.Dot(angularGlobalGunVelocity, gunRigidbody.transform.forward) * Mathf.Rad2Deg;

        //Si la suma de 1 en buffer es mayo o igual al tamanio se reinicia
        if (++_bufferIterator >= MOVE_BUFFER_SIZE)
        {
            _bufferIterator = 0;
        }

        
        _xAxisBuffer[_bufferIterator] = angularXVelocity;
        _zAxisBuffer[_bufferIterator] = angularZVelocity;
    }

    void Update()
    {
        GetInput();
        gunAnimator.SetBool(_IsLoading, grip);

        var speedX = _xAxisBuffer.Sum(num => num);
        var speedZ = _zAxisBuffer.Sum(num => num);
        
        //Debug.Log("speed Y: " + speedX);
        //Debug.Log("speed Z: " + speedZ);
      
        if (Mathf.Abs(speedX) > _config.ReloadSpeedLimit && canReload && grip)
        {
            StartCoroutine(nameof(ReloadCoroutine));
        }
        
        if (Mathf.Abs(speedZ) > changeGunSpeedLimit && grip)
        {
            StartCoroutine(NextGunCoroutine(speedZ, changeGunSpeedLimit));
            //NextGun((int)Mathf.Sign(speedZ));
        }

        if (trigger && !grip)
        {
            StartCoroutine(Shoot());
        }
    }


    private IEnumerator NextGunCoroutine(float speedZ, float absSpeedLimit)
    {
        bool result = false;
        float seconds = 0f;
        float currentZSpeed = _zAxisBuffer.Sum(num => num);
        var speedSign = (int)Mathf.Sign(speedZ);
        canReload = false;

        while (seconds < secondsToChangeWeapon)
        {
            seconds += Time.deltaTime;
            //Si el signo de la veliciada actual es contrario a la velocidad inicial
            // y la velocidad absoluta de la velocidad actual es mayor al ,limite absoluto de velocidad
            // el resultado es posotivo
            if (((int)Mathf.Sign(currentZSpeed)) != speedSign && Mathf.Abs(currentZSpeed) > absSpeedLimit)
            {
                result = true; break;
            }
            yield return null;

            currentZSpeed = _zAxisBuffer.Sum(num => num);
        }
        
        if(result)
            NextGun(speedSign);
        
        canReload = true;
    }
    private void NextGun(int moveIndex)
    {
        _magazinesLoad[_currentGun] = magazine.Value;
        _currentGun += moveIndex;
        if (_currentGun < 0) _currentGun = guns.Length - 1;
        if (_currentGun >= guns.Length) _currentGun = 0;
        
        magazine.Value = _magazinesLoad[_currentGun];
        ChangeGunData(guns[_currentGun]);

        if (!magazineText)
        {
            magazineText.color = magazine.Value == 0 ? Color.red : magazine.Value >= _config.MagazineSize ? Color.green : Color.white;
        }
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
        magazine.Value += _config.ReloadBullets;
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

            if (_config.UseMultiBirdShot)
            {
                for (int i = 0; i < _config.MultiBirdShotTimes; i++)
                {
                    var direction = MakeShoot();
                    InstantiateVisualNormal(direction);
                }
            }
            else
            {
                var direction = MakeShoot();
                InstantiateVisualNormal(direction);
            }
            

            
            if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("Shoot_0"+Random.Range(1,8));
            if(magazineText) magazineText.color = Color.white;
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

    private Vector3 MakeShoot()
    {
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

        return direction;
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
