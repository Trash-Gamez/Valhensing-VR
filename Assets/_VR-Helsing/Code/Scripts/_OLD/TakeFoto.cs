using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TakeFoto : MonoBehaviour
{

    [SerializeField] private int multiplier;
    public InputActionReference takeFotoAction;
    public Camera cam;

    private void OnEnable()
    {
        takeFotoAction.action.Enable();
        takeFotoAction.action.performed += OnTakePhotoPerformed;
    }

    private void OnDisable()
    {
        takeFotoAction.action.performed -= OnTakePhotoPerformed;
        takeFotoAction.action.Disable();
    }

    private void OnTakePhotoPerformed(InputAction.CallbackContext context)
    {
        Take();
    }

    public void Take()
    {
        Debug.Log("ScreenShot");
        string filename = "screenshot " + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png";
#if UNITY_EDITOR
        StartCoroutine(CaptureScreenshotHighRes(filename, multiplier));
#else
        ScreenCapture.CaptureScreenshot(filename, multiplier);
#endif
    }

    private IEnumerator CaptureScreenshotHighRes(string filename, int multiplier)
    {
        yield return new WaitForEndOfFrame();
        if (cam == null)
        {
            Debug.LogError("No main camera found for screenshot.");
            yield break;
        }
        int width = Screen.width * multiplier;
        int height = Screen.height * multiplier;
        RenderTexture rt = new RenderTexture(width, height, 24);
        cam.targetTexture = rt;
        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenShot.Apply();
        cam.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log($"Screenshot saved to {filename}");
    }
}
