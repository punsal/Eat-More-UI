using UnityEngine;

public enum ClickState { Pressed, Released }

public class ClickAnimationController : MonoBehaviour
{
    [Header("Animated Objects")]
    public RectTransform background;
    public RectTransform button;

    [Header("Animation Properties")]
    public float onPressedScale;

    public ClickState State { get; private set; } = ClickState.Released;

    public void OnPointerDown()
    {
        State = ClickState.Pressed;
        Vector3 scale = Vector3.one * onPressedScale;
        background.localScale = scale;
        button.localScale = scale;
    }

    public void OnPointerUp()
    {
        State = ClickState.Released;
        background.localScale = Vector3.one;
        button.localScale = Vector3.one;
    }
}
