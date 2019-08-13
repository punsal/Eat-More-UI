using UnityEngine;

public class CharacterFrameManager : MonoBehaviour
{
    [Header("Character Frames")]
    public string framePath;
    public FrameVisualController frameVisual_1;
    public FrameVisualController frameVisual_2;
    public FrameVisualController frameVisual_3;

    private ShopDataBinder shopBinder;

    private void Start()
    {
        shopBinder = GetComponentInParent<ShopDataBinder>();

        string tempFramePath = framePath + " " + (shopBinder.GetBodyIndex() + 1).ToString();
        Sprite[] frames = ItemSpawner.LoadSprites(tempFramePath);

        if (frames.Length != 0)
        {
            frameVisual_1.SetVisual(frames[0]);
            frameVisual_2.SetVisual(frames[1]);
            frameVisual_3.SetVisual(frames[2]);
        }
    }
}
