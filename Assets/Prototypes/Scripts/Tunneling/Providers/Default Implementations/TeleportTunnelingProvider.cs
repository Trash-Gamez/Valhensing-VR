using Autohand.Demo;
using UnityEngine;

public class TeleportTunnelingProvider : BaseTunnelingProvider
{
    [SerializeField]
    private OpenXRTeleporterLink _teleporter;

    public void Update()
    {
        //var isTeleporting = _teleporter.ExecutingTeleport;
        var isTeleporting = _teleporter.startTeleportAction.reference.action.ReadValue<bool>();
        UpdateLocomotionPhase(isTeleporting);
    }
}
