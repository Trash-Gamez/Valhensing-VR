using Autohand;
using Sirenix.OdinInspector;
using UnityEngine;
public class NEW_GunHolder : MonoBehaviour
{
    private enum GunHolderType {Main, Secondary}
    
    [SerializeField] private Grabbable gunGrabbable, secondaryGunGrabbable;
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
            if (!handToChange.CanGrab(gunGrabbable)) return;
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
        Vector3 newScale = Vector3.zero;
        switch (gunHolderType)
        {
            case GunHolderType.Main:
                _currentHoldingHand = handToChange;
                
                _currentHoldingHand.ForceGrab(gunGrabbable);
                break;
            case GunHolderType.Secondary:
                _secondaryHoldingHand = handToChange;
                _secondaryHoldingHand.ForceGrab(secondaryGunGrabbable);
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
                gunGrabbable.transform.position = behindHeadPos.position;
                break;
            case GunHolderType.Secondary:
                _secondaryHoldingHand.ForceReleaseGrab();
                _secondaryHoldingHand = null;
                secondaryGunGrabbable.transform.position = behindHeadPos.position;
                break;
        }
    }
}
