using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShoot : MonoBehaviour
{
    [SerializeField] private int multiplier;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("ScreenShot");
            ScreenCapture.CaptureScreenshot("screenshot " + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png", multiplier);
        }
    }
}
