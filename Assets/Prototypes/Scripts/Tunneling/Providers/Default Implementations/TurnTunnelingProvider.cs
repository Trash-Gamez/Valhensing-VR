using System;
using Autohand;

public abstract class TurnTunnelingProvider : BaseTunnelingProvider
{
    abstract protected RotationType RotationType { get; }

    private bool isTurning = false;
    
    public void Update()
    {
        var isCorrectType = System.AutohandPlayer.rotationType == RotationType;
        var isTurningCorrectly = isTurning && isCorrectType;

        UpdateLocomotionPhase(isTurningCorrectly);

        isTurning = false;
    }


    private void OnSnapTurn(AutoHandPlayer player)
    {
        if (RotationType != RotationType.snap) return;

        isTurning = true;
    }
    
    private void OnSmoothTurn(AutoHandPlayer player)
    {
        if (RotationType != RotationType.smooth) return;
        
        isTurning = true;
    }
    
    private void OnEnable()
    {
        System.AutohandPlayer.OnSnapTurn += OnSnapTurn;
        System.AutohandPlayer.OnSmoothTurn += OnSmoothTurn;
    }



    private void OnDisable()
    {
        System.AutohandPlayer.OnSnapTurn -= OnSnapTurn;
        System.AutohandPlayer.OnSmoothTurn -= OnSmoothTurn;
    }
}
