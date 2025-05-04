using Autohand;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class NEW_GunHolder : MonoBehaviour
{
    private enum GunHolderType {Main, Secondary}
    
    [SerializeField] private Grabbable gunGrabbable, secondaryGunGrabbable;
    [SerializeField] private Hand leftHand, rightHand;
    [SerializeField] private InputActionProperty changeGunLeft, changeGunRight;

    [SerializeField] private Transform behindHeadPos;
    public Hand LeftHand => leftHand;
    public Hand RightHand => rightHand;

    private bool _hasSecondWeapon;

    private void Start()
    {
        HandleGunChange(rightHand);
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

    private Hand _currentHoldingHand, _secondaryHoldingHand;

    public void AlterGun(Hand toHand)
    {
        
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
            else
                SwitchWeapon(GunHolderType.Main, handToChange);
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
    
    /*
    private Hand GetHandFromHolderType(GunHolderType holderType) => holderType switch
    {
        GunHolderType.Main => _currentHoldingHand,
        GunHolderType.Secondary => _secondaryHoldingHand,
    };
    
    private void GetGunGrabbableFromHolderType(GunHolderType holderType, ref Grabbable grabbableRef)
    {
        switch (holderType)
        {
            case GunHolderType.Main:
                grabbableRef = gunGrabbable;
                break;
            case GunHolderType.Secondary:
                grabbableRef = secondaryGunGrabbable;
                break;
        }
    }
    */

    private void OnEnable()
    {
        Debug.Log("Enable");
        changeGunLeft.action.Enable();
        changeGunRight.action.Enable();
        changeGunLeft.action.performed += delegate { HandleGunChange(leftHand); };
        changeGunRight.action.performed += delegate { HandleGunChange(rightHand); };
    }
}
