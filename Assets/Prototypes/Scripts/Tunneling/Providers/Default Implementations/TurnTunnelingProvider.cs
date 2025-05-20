using System;
using Autohand;

public abstract class TurnTunnelingProvider : BaseTunnelingProvider
{
    abstract protected RotationType RotationType { get; }

    public void Update()
    {
        var isTurning = System.AutohandPlayer.IsTurning();
        var isCorrectType = System.AutohandPlayer.rotationType == RotationType;
        var isTurningCorrectly = isTurning && isCorrectType;

        UpdateLocomotionPhase(isTurningCorrectly);
    }

    private void OnEnable()
    {

    }
}
