using UnityEngine;

public class TEST_GunState : TEST_VarGunText
{
    protected override string GetText()
    {
        return "State: " + gun.CurrentState.GetType().Name;
    }
}
