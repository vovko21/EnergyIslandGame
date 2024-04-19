using System;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    public KeyCode screenShotButton;

    void Update()
    {
        if (Input.GetKeyDown(screenShotButton))
        {
            ScreenCapture.CaptureScreenshot("screenshot " + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png");
            Debug.Log("A screenshot was taken!");
        }
    }
}
