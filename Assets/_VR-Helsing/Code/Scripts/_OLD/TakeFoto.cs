using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TakeFoto : MonoBehaviour
{
    [SerializeField] private int multiplier;

    private InputAction screenshotAction;

    private void OnEnable()
    {
        screenshotAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/space");
        screenshotAction.performed += OnScreenshot;
        screenshotAction.Enable();
    }

    private void OnDisable()
    {
        screenshotAction.performed -= OnScreenshot;
        screenshotAction.Disable();
        screenshotAction.Dispose();
    }

    private void OnScreenshot(InputAction.CallbackContext context)
    {
        Debug.Log("ScreenShot");
        ScreenCapture.CaptureScreenshot("screenshot " + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png", multiplier);
    }
}
