using UnityEngine;

public class MoveTunnelingProvider : BaseTunnelingProvider
{
    public void Update()
    {
        var isMoving = System.AutohandPlayer.MoveDirection.sqrMagnitude > Mathf.Epsilon;
        UpdateLocomotionPhase(isMoving);
    }
}
