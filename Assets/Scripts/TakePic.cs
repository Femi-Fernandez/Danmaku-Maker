using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePic : MonoBehaviour
{

    public void takeScreenshot()
    {

        ScreenCapture.CaptureScreenshot("DanmakuMaker.png", 5);

    }

}
