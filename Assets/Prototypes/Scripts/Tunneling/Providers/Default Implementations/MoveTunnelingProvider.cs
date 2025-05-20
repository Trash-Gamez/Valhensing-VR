using UnityEngine;

public class MoveTunnelingProvider : BaseTunnelingProvider
{
    public void Update()
    {
        //var isMoving = System.AutohandPlayer.MoveDirection.sqrMagnitude > Mathf.Epsilon;
        var isMoving = System.AutohandPlayer.body.linearVelocity.sqrMagnitude > Mathf.Epsilon;
        UpdateLocomotionPhase(isMoving);
    }
}
