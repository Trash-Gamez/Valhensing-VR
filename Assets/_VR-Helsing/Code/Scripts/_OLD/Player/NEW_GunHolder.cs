using _VR_Helsing.Gun;
using Autohand;
using Sirenix.OdinInspector;
using UnityEngine;
public class NEW_GunHolder : MonoBehaviour
{
    private enum GunHolderType {Main, Secondary}

    [SerializeField] private GunHandler gunHandler, secondaryGunHandler;
    [SerializeField] private Hand leftHand, rightHand;

    [SerializeField] private Transform behindHeadPos;

    [SerializeField] private bool equipMainWeaponOnStart = true;
    [SerializeField] private bool startWithSecondWeapon;
    
    private Hand _currentHoldingHand, _secondaryHoldingHand;
    private bool _hasSecondWeapon;

    private void Start()
    {
        if(equipMainWeaponOnStart)HandleGunChange(rightHand);
        _hasSecondWeapon = startWithSecondWeapon;
    }
    
    [VerticalGroup("Editor")]
    [Button("Equip Weapon", DisplayParameters = true)]
    private void EquipMainWeapon(bool isLeft)
    {
        HandleGunChange(isLeft ? leftHand : rightHand);
    }

    [VerticalGroup("Editor")]
    [Button("DESBLOQUEA SEGUNDA ARMA")]
    public void UnlockSecondWeapon()
    {
        _hasSecondWeapon = true;
    }


    public void AlterGun(Hand toHand)
    {
        HandleGunChange(toHand);
    }
    
    private void HandleGunChange(Hand handToChange)
    {
        if (_hasSecondWeapon)
        {
            HandleSecondGunChange(handToChange);
            return;
        }
        
        if (_currentHoldingHand)
        {
            if (handToChange == _currentHoldingHand)
                HideWeapon(GunHolderType.Main);
            //else
                //SwitchWeapon(GunHolderType.Main, handToChange);
            // Si se requiere cambio de arma, descomentar esta seccion
        }
        else
        {
            if (!handToChange.CanGrab(gunHandler.Grabbable)) return;
            ShowWeapon(GunHolderType.Main, handToChange);
        }
    }

    private void HandleSecondGunChange(Hand handToChange)
    {
        if (handToChange == _currentHoldingHand)
        {
            HideWeapon(GunHolderType.Main);
        }
        else if (handToChange == _secondaryHoldingHand)
        {
            HideWeapon(GunHolderType.Secondary);    
        }else
        {
            ChooseShowWeapon(handToChange);
        }
    }

    private void ChooseShowWeapon(Hand handToChange)
    {
        Debug.Log("Se elige el mostrar arma");
        if (!_currentHoldingHand)
        {
            ShowWeapon(GunHolderType.Main, handToChange);
        }else if (!_secondaryHoldingHand)
        {
            ShowWeapon(GunHolderType.Secondary, handToChange);
        }
    }

    private void SwitchWeapon(GunHolderType gunHolderType,Hand handToChange)
    {
        switch (gunHolderType)
        {
            case GunHolderType.Main:
                _currentHoldingHand.ForceReleaseGrab();
                break;
            case GunHolderType.Secondary:
                _secondaryHoldingHand.ForceReleaseGrab();
                break;
        }

        ShowWeapon(gunHolderType, handToChange);
    }
    
    private void ShowWeapon(GunHolderType gunHolderType, Hand handToChange)
    {
        
        switch (gunHolderType)
        {
            case GunHolderType.Main:
                _currentHoldingHand = handToChange;        
                _currentHoldingHand.ForceGrab(gunHandler.Grabbable);
                
                gunHandler.SetEquipped(true);
                
                break;
            case GunHolderType.Secondary:
                _secondaryHoldingHand = handToChange;
                _secondaryHoldingHand.ForceGrab(secondaryGunHandler.Grabbable);
                
                secondaryGunHandler.SetEquipped(true);
                
                break;
        }
    }

    private void HideWeapon(GunHolderType gunHolderType)
    {
        switch (gunHolderType)
        {
            case GunHolderType.Main:
                _currentHoldingHand.ForceReleaseGrab();
                _currentHoldingHand = null;
                gunHandler.transform.position = behindHeadPos.position;
                
                gunHandler.SetEquipped(false);
                
                break;
            case GunHolderType.Secondary:
                _secondaryHoldingHand.ForceReleaseGrab();
                _secondaryHoldingHand = null;
                secondaryGunHandler.transform.position = behindHeadPos.position;
                
                secondaryGunHandler.SetEquipped(false);
                
                break;
        }
    }
}
