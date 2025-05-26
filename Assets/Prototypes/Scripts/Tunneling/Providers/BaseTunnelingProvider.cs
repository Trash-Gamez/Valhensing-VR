using UnityEngine;

public abstract class BaseTunnelingProvider : TunnelingProvider
{
    protected void UpdateLocomotionPhase(bool isMoving)
    {
        switch (LocomotionPhase)
        {
            case LocomotionPhase.Idle:
            case LocomotionPhase.Started:
                if (isMoving)
                    LocomotionPhase = LocomotionPhase.Moving;
                break;
            case LocomotionPhase.Moving:
                if (!isMoving)
                    LocomotionPhase = LocomotionPhase.Done;
                break;
            case LocomotionPhase.Done:
                LocomotionPhase = isMoving ? LocomotionPhase.Moving : LocomotionPhase.Idle;
                break;
            default:
                Debug.LogError($"Unhandled LocomotionPhase={LocomotionPhase}");
                break;
        }
    }
}
