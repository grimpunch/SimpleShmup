using UnityEngine;
using System.Collections;

public class PixelPerfectScale : MonoBehaviour
{
    public int screenVerticalPixels = 256;

    public bool preferUncropped = true;

    private float screenPixelsY;

    private float ratio;

    void Update()
    {
        if(preferUncropped) {
            screenPixelsY = (float)Screen.height;
            float screenRatio = screenPixelsY / screenVerticalPixels;
            ratio = Mathf.Floor(screenRatio) / screenRatio;
            transform.localScale = Vector3.one * ratio;
        } else {
            screenPixelsY = (float)Screen.height;
            float screenRatio = screenPixelsY / screenVerticalPixels;
            ratio = Mathf.Ceil(screenRatio) / screenRatio;
            transform.localScale = Vector3.one * ratio;
        }
    }
}
