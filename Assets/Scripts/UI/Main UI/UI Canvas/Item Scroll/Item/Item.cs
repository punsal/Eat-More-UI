using UnityEngine;
using ScrollItemData;

/// <summary>
/// Contains information about ScrollView Items.
/// </summary>
/// <remarks>
/// Item related visual and logical operations must be performed via Item methods.
/// </remarks>

public class Item : MonoBehaviour
{
    #region Data & Getter Setter
    /// <summary>
    /// Stores different type of ItemData.
    /// </summary>
    private ItemData data;

    /// <summary>
    /// ItemData property to provide information.
    /// </summary>
    /// <value>
    /// return ItemData contains : Map, Body, Extra, Special, Flag datas.
    /// </value>
    public ItemData Data
    {
        get
        {
            return data;
        }
        set
        {
            data = value;
        }
    }

    /// <summary>
    /// Sets "ItemData.ITEMTYPE.basicData.state". Best practice for setting MapType ItemState.
    /// </summary>
    /// <param name="state"> After mapData.State is set, Item:ApplyIconVisual() call will be a good practice. </param>
    public void SetItemState(ItemState state)
    {
        switch (data.type)
        {
            case ItemType.Map:
                data.mapData.basicData.state = state;
                break;
            case ItemType.Body:
                data.bodyData.basicData.state = state;
                break;
            case ItemType.Extra:
                data.extraData.basicData.state = state;
                break;
            case ItemType.Special:
                data.specialData.basicData.state = state;
                break;
            case ItemType.Flag:
                data.flagData.basicData.state = state;
                break;
            default:
                throw new System.Exception("ItemData.Type is inappropriate.");
        }
    }

    /// <summary>
    /// Sets "ItemData.SHOPITEM.payableData.state". Don't use for non-ShopItemDatas like MapType.
    /// </summary>
    /// <param name="state">Beware, returns an exception if a MapType calls method.</param>
    /// <seealso cref="Item"/>
    public void SetShopItemState(ShopItemState state)
    {
        switch (data.type)
        {
            case ItemType.Map:
                throw new System.Exception("Item:Data.ItemType is inappropriate. Read the summary of function.");
            case ItemType.Body:
                data.bodyData.payableData.state = state;
                break;
            case ItemType.Extra:
                data.extraData.payableData.state = state;
                break;
            case ItemType.Special:
                data.specialData.payableData.state = state;
                break;
            case ItemType.Flag:
                break;
            default:
                throw new System.Exception("ShopItemData.Type is inappropriate.");
        }
    }
    #endregion

    #region Component Controllers & Communicators
    private IVisualController visualController;

    public RectTransform Rect { get { return GetComponent<RectTransform>(); } }
    #endregion

    #region Visual Controllers
    /// <summary>
    /// Applies icon data to its visual.(For MapData, after checking its ItemState).
    /// </summary>
    /// <seealso cref="IconVisualController.SetVisual(Sprite)">
    /// At the end ApplyIconVisual() method calls VisualController:SetVisual(Sprite) method.
    /// </seealso>
    public void ApplyIconVisual()
    {
        visualController = GetComponentInChildren<IconVisualController>();
        Sprite visual;

        switch (data.type)
        {
            case ItemType.Map:
                switch (data.mapData.basicData.state)
                {
                    case ItemState.Locked: visual = data.mapData.locked; break;
                    case ItemState.Unlocked: visual = data.mapData.basicData.icon; break;
                    default: throw new System.Exception("Item.data.state has a problem");
                }
                break;
            case ItemType.Body: visual = data.bodyData.basicData.icon; break;
            case ItemType.Extra: visual = data.extraData.basicData.icon; break;
            case ItemType.Special: visual = data.specialData.basicData.icon; break;
            case ItemType.Flag: visual = data.flagData.basicData.icon; break;
            default:
                visual = Sprite.Create(new Texture2D(200, 200), new Rect(Vector2.zero, Vector2.one * 200f), Vector2.one * 0.5f);
                Debug.Log("Control ItemType in debug mod. Track it backward.");
                break;
        }
        visualController.SetVisual(visual);
    }

    public void SetAnimationVisual(Sprite sprite)
    {
        visualController = GetComponentInChildren<AnimationVisualController>();
        visualController.SetVisual(sprite);
    }

    public void SetBackgroundVisual(Sprite sprite)
    {
        visualController = GetComponentInChildren<BackgroundVisualController>();
        visualController.SetVisual(sprite);
    }

    public void SetCheckVisual(Sprite sprite)
    {   
        visualController = GetComponentInChildren<CheckVisualController>();
        visualController.SetVisual(sprite);
    }
    #endregion
}