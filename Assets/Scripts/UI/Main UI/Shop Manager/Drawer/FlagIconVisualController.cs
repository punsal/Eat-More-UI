using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sets given Sprite to its Image components Sprite attribute. It can be called dynamically.
/// </summary>

public class FlagIconVisualController : MonoBehaviour, IVisualController
{
    private Image visual;

    public void SetVisual(Sprite sprite)
    {
        visual = GetComponent<Image>();
        visual.sprite = sprite;
    }
}
