using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sets given Sprite to its Image components Sprite attribute. It generally is called when Scene loads.
/// </summary>

public class AnimationVisualController : MonoBehaviour, IVisualController
{
    private Image selectVisual;

    public void SetVisual(Sprite sprite)
    {
        selectVisual = GetComponent<Image>();
        selectVisual.sprite = sprite;
    }
}
