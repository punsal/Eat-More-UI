using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sets dependent Sprite of its Frame component of Character
/// </summary>

public class FrameVisualController : MonoBehaviour, IVisualController
{
    private Image visual;

    public void SetVisual(Sprite sprite)
    {
        visual = GetComponent<Image>();
        visual.sprite = sprite;
    }
}
