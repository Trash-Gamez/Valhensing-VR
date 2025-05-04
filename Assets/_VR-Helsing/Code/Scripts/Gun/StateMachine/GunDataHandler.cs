using RacTools.Variables;
using UnityEngine;

public class GunDataHandler : MonoBehaviour
{
    private readonly int[] _magazinesLoad = new int[3];
    
    [SerializeField] private Renderer[] gunRenderers;
    [SerializeField] private GunData[] guns;
    [SerializeField] private GunData currentData;
    
    [Header("Gun Variables")]
    [SerializeField]
    private Variable<int> magazine;
    
    private int _currentGun = 0;
    private GunConfig _config;
    
    public Variable<int> Magazine => magazine;
    public int CurrentGun { get => _currentGun; set => _currentGun = value; }
    public GunData[] Guns => guns;
    public GunConfig Config => _config;
    public int[] MagazinesLoad => _magazinesLoad;
    
    private void Start()
    {
        magazine.Value = 0;
        _config = currentData.Config;
    }
    
    public void NextGun(int moveIndex)
    {
        MagazinesLoad[CurrentGun] = magazine.Value;
        CurrentGun += moveIndex;
        if (CurrentGun < 0) CurrentGun = guns.Length - 1;
        if (CurrentGun >= guns.Length) CurrentGun = 0;
        
        magazine.Value = MagazinesLoad[CurrentGun];
        ChangeGunData(Guns[CurrentGun]);
    }

    private void ChangeGunData(GunData newData)
    {
        if (newData.indexMesh < 0 || newData.indexMesh >= gunRenderers.Length) return;
            
        currentData = newData;
        _config = currentData.Config;

        
        DisableAllRenderers();
        gunRenderers[currentData.indexMesh].enabled = true;
    }
    
    private void DisableAllRenderers()
    {
        for (int i = 0; i < gunRenderers.Length; i++)
        {
            gunRenderers[i].enabled = false;
        }
    }
}
