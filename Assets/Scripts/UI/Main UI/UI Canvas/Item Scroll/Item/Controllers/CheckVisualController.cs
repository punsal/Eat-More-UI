using UnityEngine;
using UnityEngine.UI;

public class CheckVisualController : MonoBehaviour, IVisualController
{
    private Image visual;

    public void SetVisual(Sprite sprite)
    {
        visual = GetComponent<Image>();
        visual.sprite = sprite;
    }
}
