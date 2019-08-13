using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIUtility;

/// <summary>
/// It makes sure UI Headers fit in given aspect ratio of device.
/// </summary>
public class ResponsiveUIController : MonoBehaviour
{
    public float wideScreenY = 0f;
    public float ultraWideScreenY = -56f;
    public bool isTesting;


    private RectTransform rect;
    private float aspectRatio = 0f;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        if (isTesting)
        {
            aspectRatio = (1.0f * Screen.height) / (1.0f * Screen.width);
            if (2.1f < aspectRatio && aspectRatio < 2.2f)
            {
                Debug.Log("iPhone X+ is detected!");
                rect.anchoredPosition = rect.anchoredPosition.ModifyV2(null, ultraWideScreenY);
            }
            else
            {
                Debug.Log("Generic Wide screen is detected!");
                rect.anchoredPosition = rect.anchoredPosition.ModifyV2(null, wideScreenY);
            }
        }
        else
        {
            switch (SystemInfo.deviceType)
            {
                case DeviceType.Handheld:
                    aspectRatio = (1.0f * Screen.height) / (1.0f * Screen.width);
                    if (2.1f < aspectRatio && aspectRatio < 2.2f)
                    {
                        Debug.Log("iPhone X+ is detected!");
                        rect.anchoredPosition = rect.anchoredPosition.ModifyV2(null, ultraWideScreenY);
                    }
                    else
                    {
                        Debug.Log("Generic Wide screen is detected!");
                        rect.anchoredPosition = rect.anchoredPosition.ModifyV2(null, wideScreenY);
                    }
                    break;
                default:
                    Debug.Log("No screen is detected!");
                    break;
            }
        }
    }
}
