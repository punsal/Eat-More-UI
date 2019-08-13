using UnityEngine;

public class FlagIconController : MonoBehaviour
{
    public IconVisualController flagbutton;

    #region Components
    private ShopDataBinder binder;
    private ItemScrollManager scrollManager;
    private SnapManager snapManager;
    #endregion

    #region Cache
    private int index;
    private Item[] items;
    private Sprite sprite;
    #endregion

    private void Start()
    {
        binder = GetComponentInParent<ShopDataBinder>();
        scrollManager = GetComponent<ItemScrollManager>();
        snapManager = GetComponent<SnapManager>();
    }

    private void Update()
    {
        UpdateFlagIcon();
    }

    private void UpdateFlagIcon()
    {
        index = snapManager.SnapIndex;
        items = scrollManager.GetItems();
        sprite = items[index].Data.mapData.basicData.icon;

        flagbutton.SetVisual(sprite);
        binder.SetFlagIcon(index);
    }
}
