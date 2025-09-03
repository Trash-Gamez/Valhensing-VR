using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TakeFoto : MonoBehaviour
{

    [SerializeField] private int multiplier;
	

    private void Start()
    {
        Debug.Log("ScreenShot");
        ScreenCapture.CaptureScreenshot("screenshot " + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png", multiplier);
    }
}
