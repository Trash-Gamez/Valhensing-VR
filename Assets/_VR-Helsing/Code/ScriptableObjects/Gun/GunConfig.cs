using UnityEngine;

[CreateAssetMenu(fileName = "GunConfig", menuName = "Scriptable Objects/Gun/GunConfig")]
public class GunConfig : ScriptableObject
{
    [field: Header("Magazine")]
    [field: SerializeField] public bool SingleShot { get; private set; }
    [field: SerializeField] public int MagazineSize { get; private set; } = 30;
    
    [field: Header("Reload")]
    [field: SerializeField] public float ReloadTime { get; private set; } = 0.25f;
    [field: SerializeField] public float ReloadSpeedLimit { get; private set; } = 12;
    
    [field: Header("Shoot")]
    [field: SerializeField] public float ShootingSpeed { get; private set; } = 0.1f;
    [field: SerializeField] public float Spread { get; private set; } = 0;
    [field: SerializeField] public float FireRange { get; private set; } = 50;
    [field: SerializeField] public float RecoilForce {get; private set;} = 50;
    [field: SerializeField] public int Damage { get; private set; } = 1;


}
