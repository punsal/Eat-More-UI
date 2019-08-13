#pragma warning disable 649
using UnityEngine;

public class ItemScrollManager : MonoBehaviour
{
    public ItemType type = ItemType.Map;
    public ExtraItemType extraType = ExtraItemType.Down;

    [SerializeField]
    private PointManager pointManager;
    public PointManager GetPointManager() { return pointManager; }

    [SerializeField]
    private RectTransform itemContainer;
    public RectTransform GetItemContainer() { return itemContainer; }

    private ItemSpawner spawner;

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        SetContainerPosition();
    }

    public void Initialize()
    {
        spawner = GetComponent<ItemSpawner>();
        spawner.EmptyContainer();
        spawner.LoadItem();
        spawner.Create();
    }

    //Sets Container Position with PlayerData Stats
    private void SetContainerPosition()
    {
        float position = -1;
        position *= spawner.ItemDistance;
        switch (type)
        {
            case ItemType.Map:
                position *= GetComponentInParent<UIDataBinder>().GetMapIndex();
                break;
            case ItemType.Body:
                position *= GetComponentInParent<ShopDataBinder>().GetBodyIndex();
                break;
            case ItemType.Extra:
                switch (extraType)
                {
                    case ExtraItemType.Top:
                        position *= GetComponentInParent<ShopDataBinder>().GetTopIndex();
                        break;
                    case ExtraItemType.Front:
                        position *= GetComponentInParent<ShopDataBinder>().GetFrontIndex();
                        break;
                    case ExtraItemType.Down:
                        position *= GetComponentInParent<ShopDataBinder>().GetDownIndex();
                        break;
                    default:
                        position *= 0f;
                        break;
                }
                break;
            case ItemType.Special:
                position *= GetComponentInParent<ShopDataBinder>().GetSpecialIndex();
                break;
            case ItemType.Flag:
                ShopDataBinder shopBinder = GetComponentInParent<ShopDataBinder>();
                position *= shopBinder.GetFlagIcon();
                break;
            default:
                position *= 0f;
                break;
        }

        itemContainer.anchoredPosition = new Vector2(position, 0f);
    }

    //Generic Method To Find Items in Given Container
    public Item[] GetItems()
    {
        Item[] items = new Item[itemContainer.childCount];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = itemContainer.GetChild(i).GetComponent<Item>();
        }

        return items;
    }

}
