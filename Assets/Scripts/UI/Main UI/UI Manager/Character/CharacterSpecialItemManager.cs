using UnityEngine;

public class CharacterSpecialItemManager : MonoBehaviour
{
    [Header("Character Special Frames")]
    public string specialFramePath;
    public ExtraVisualController specialFrameVisual_1;
    public ExtraVisualController specialFrameVisual_2;
    public ExtraVisualController specialFrameVisual_3;

    private ShopDataBinder shopBinder;

    private void Start()
    {
        shopBinder = GetComponentInParent<ShopDataBinder>();

        string tempFramePath = specialFramePath + " " + shopBinder.GetSpecialIndex().ToString();
        Sprite[] frames = ItemSpawner.LoadSprites(tempFramePath);

        if(frames.Length != 0)
        {
            specialFrameVisual_1.SetVisual(frames[0]);
            specialFrameVisual_2.SetVisual(frames[1]);
            specialFrameVisual_3.SetVisual(frames[2]);
        }
    }
}
