using System;
using UnityEngine;

public class CharacterItemManager : MonoBehaviour
{
    [Header("Top Item")]
    public string topPath;
    public ExtraVisualController topVisual;

    [Header("Front Item")]
    public string frontPath;
    public ExtraVisualController frontVisual;

    [Header("Down Item")]
    public string downPath;
    public ExtraVisualController downVisual;

    private ShopDataBinder shopBinder;

    // Start is called before the first frame update
    void Start()
    {
        shopBinder = GetComponentInParent<ShopDataBinder>();

        Sprite[] sprites = ItemSpawner.LoadSprites(topPath);
        topVisual.SetVisual(sprites[shopBinder.GetTopIndex()]);
        Debug.Log("Top : " + shopBinder.GetTopIndex());

        sprites = ItemSpawner.LoadSprites(frontPath);
        frontVisual.SetVisual(sprites[shopBinder.GetFrontIndex()]);

        sprites = ItemSpawner.LoadSprites(downPath);
        downVisual.SetVisual(sprites[shopBinder.GetDownIndex()]);
        Debug.Log("Down : " + shopBinder.GetDownIndex());

        sprites = null;

        GC.Collect();
    }
}
