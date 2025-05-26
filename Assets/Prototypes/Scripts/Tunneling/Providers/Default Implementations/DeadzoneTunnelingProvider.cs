using UnityEngine;

public class DeadzoneTunnelingProvider : BaseTunnelingProvider
{
    public void SetIsInDeadZone(bool isInDeadZone)
    {
        UpdateLocomotionPhase(isInDeadZone);
    }
}
