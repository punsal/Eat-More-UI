using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundVisualController : MonoBehaviour, IVisualController
{
    private Image backgroundVisual;

    public void SetVisual(Sprite sprite)
    {
        backgroundVisual = GetComponent<Image>();
        backgroundVisual.sprite = sprite;
    }

    public void SetColor(Color bgColor)
    {
        backgroundVisual = GetComponent<Image>();
        backgroundVisual.color = bgColor;
    }
}
