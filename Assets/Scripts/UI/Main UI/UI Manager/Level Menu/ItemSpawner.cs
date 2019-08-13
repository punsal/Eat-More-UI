#pragma warning disable 649

using UnityEngine;
using ScrollItemData;

[ExecuteAlways]
public class ItemSpawner : MonoBehaviour
{

    [SerializeField] private RectTransform container;
    [SerializeField] private RectTransform itemPrefab;

    public ItemType type = ItemType.Map;
    public ExtraItemType extraType = ExtraItemType.Top;
    [SerializeField] private string iconPath;
    [SerializeField] private string trimmingPath = "map_";
    [SerializeField] private string animationPath;
    [SerializeField] private string backgroundPath;
    [SerializeField] private string checkPath;
    [SerializeField] private string lockedPath;
    [SerializeField] private string framePath;
    [SerializeField] private string extraPath;
    [SerializeField] private string specialPath;

    [SerializeField] private string itemName = "Item";
    [SerializeField] private PointManager pointManager;
    [SerializeField] private float itemDistance;
    public string ItemName { get => itemName; }
    public float ItemDistance { get => itemDistance; }

    private Sprite[] icons;
    private Sprite[] animations;
    private Sprite[] backgrounds;
    private Sprite[] checks;
    private Sprite[] lockeds;
    private Sprite[] frames;
    private Sprite[] extras;
    private Sprite[] specialFrames;

    private ItemData[] itemDatas;

    public bool IsReadyToCreate { get; private set; } = false;

    //Generic method to release all child of container
    public void EmptyContainer()
    {
        if (container.transform.childCount < 1)
        {
            Debug.LogWarning("ItemContainer is already Empty!");
            return;
        }
        else
        {
            while (container.transform.childCount > 0)
            {
                DestroyImmediate(container.transform.GetChild(0).gameObject);
            }
        }
        IsReadyToCreate = false;
    }

    public void LoadItem()
    {
        string iconName;
        switch (type)
        {
            case ItemType.Map:
            {
                icons = LoadSprites(iconPath);
                lockeds = LoadSprites(lockedPath);
                animations = LoadSprites(animationPath);
                backgrounds = LoadSprites(backgroundPath);

                itemDatas = new ItemData[icons.Length];
                for (int i = 0; i < itemDatas.Length; i++)
                {
                    itemDatas[i].type = ItemType.Map;
                    itemDatas[i].mapData = new MapItemData();

                    itemDatas[i].mapData.basicData.icon = icons[i];

                    iconName = icons[i].name;
                    iconName = iconName.TrimStart(trimmingPath.ToCharArray());
                    itemDatas[i].mapData.basicData.requirement = System.Convert.ToInt32(iconName);
                    itemDatas[i].mapData.basicData.state = ItemState.Unlocked;

                    itemDatas[i].mapData.animatableData.animation = animations[0];
                    itemDatas[i].mapData.animatableData.background = backgrounds[0];

                    itemDatas[i].mapData.locked = lockeds[i];
                }
                break;
            }
            case ItemType.Body:
            {
                icons = LoadSprites(iconPath);
                checks = LoadSprites(checkPath);

                itemDatas = new ItemData[icons.Length];
                for (int i = 0; i < itemDatas.Length; i++)
                {
                    itemDatas[i].type = ItemType.Body;
                    itemDatas[i].bodyData = new BodyItemData();

                    itemDatas[i].bodyData.basicData.icon = icons[i];

                    iconName = icons[i].name;
                    string padding = (i + 1).ToString("D3");
                    string requirementTrimmer = trimmingPath + padding + "_";
                    iconName = iconName.Substring(requirementTrimmer.Length);
                    itemDatas[i].bodyData.basicData.requirement = System.Convert.ToInt32(iconName);
                    itemDatas[i].bodyData.basicData.state = ItemState.Unlocked;

                    itemDatas[i].bodyData.checkableData.check = checks[0];

                    if (PlayerData.instance.GetUnlockedBodies().Contains(i))
                    {
                        itemDatas[i].bodyData.payableData.state = ShopItemState.Unlocked;
                    }
                    else
                    {
                        itemDatas[i].bodyData.payableData.state = ShopItemState.Locked;
                    }

                    string tempFramePath = framePath + " " + (i + 1).ToString();
                    frames = LoadSprites(tempFramePath);
                    if (frames.Length != 0)
                    {
                        itemDatas[i].bodyData.frame_1 = frames[0];
                        itemDatas[i].bodyData.frame_2 = frames[1];
                        itemDatas[i].bodyData.frame_3 = frames[2];
                    }
                    else
                    {
                        Debug.Log("Temp Frame Path : " + tempFramePath);
                    }
                }
                break;
            }
            case ItemType.Extra:
            {
                icons = LoadSprites(iconPath);
                checks = LoadSprites(checkPath);
                extras = LoadSprites(extraPath);

                itemDatas = new ItemData[icons.Length];
                for (int i = 0; i < itemDatas.Length; i++)
                {
                    itemDatas[i].type = ItemType.Extra;
                    itemDatas[i].extraData = new ExtraItemData();

                    itemDatas[i].extraData.basicData.icon = icons[i];

                    iconName = icons[i].name;
                    string padding = (i + 1).ToString("D3");
                    string requirementTrimmer = trimmingPath + padding + "_";
                    iconName = iconName.Substring(requirementTrimmer.Length);
                    itemDatas[i].extraData.basicData.requirement = System.Convert.ToInt32(iconName);
                    itemDatas[i].extraData.basicData.state = ItemState.Unlocked;

                    itemDatas[i].extraData.checkableData.check = checks[0];

                    if (Application.isPlaying)
                    {
                        switch (extraType)
                        {
                            case ExtraItemType.Top:
                                if (PlayerData.instance.GetUnlockedTopAccessories().Contains(i))
                                {
                                    itemDatas[i].extraData.payableData.state = ShopItemState.Unlocked;
                                }
                                else
                                {
                                    itemDatas[i].extraData.payableData.state = ShopItemState.Locked;
                                }
                                break;
                            case ExtraItemType.Front:
                                if (PlayerData.instance.GetUnlockedFrontAccessories().Contains(i))
                                {
                                    itemDatas[i].extraData.payableData.state = ShopItemState.Unlocked;
                                }
                                else
                                {
                                    itemDatas[i].extraData.payableData.state = ShopItemState.Locked;
                                }
                                break;
                            case ExtraItemType.Down:
                                if (PlayerData.instance.GetUnlockedBottomAccessories().Contains(i))
                                {
                                    itemDatas[i].extraData.payableData.state = ShopItemState.Unlocked;
                                }
                                else
                                {
                                    itemDatas[i].extraData.payableData.state = ShopItemState.Locked;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        itemDatas[i].extraData.payableData.state = ShopItemState.Locked;
                    }


                    itemDatas[i].extraData.extra = extras[i];
                }
                break;
            }
            case ItemType.Special:
                icons = LoadSprites(iconPath);
                checks = LoadSprites(checkPath);

                itemDatas = new ItemData[icons.Length];
                for (int i = 0; i < itemDatas.Length; i++)
                {
                    itemDatas[i].type = ItemType.Special;
                    itemDatas[i].specialData = new SpecialItemData();

                    itemDatas[i].specialData.basicData.icon = icons[i];

                    iconName = icons[i].name;
                    string padding = (i + 1).ToString("D3");
                    string requirementTrimmer = trimmingPath + padding + "_";
                    iconName = iconName.Substring(requirementTrimmer.Length);
                    itemDatas[i].specialData.basicData.requirement = System.Convert.ToInt32(iconName);
                    itemDatas[i].specialData.basicData.state = ItemState.Unlocked;

                    itemDatas[i].specialData.checkableData.check = checks[0];

                    if (Application.isPlaying)
                    {

                        if (PlayerData.instance.GetUnlockedSpecialAccessories().Contains(i))
                        {
                            itemDatas[i].specialData.payableData.state = ShopItemState.Unlocked;
                        }
                        else
                        {
                            itemDatas[i].specialData.payableData.state = ShopItemState.Locked;
                        }
                    }
                    else
                    {
                        itemDatas[i].specialData.payableData.state = ShopItemState.Locked;
                    }

                    string tempFramePath = specialPath + " " + i.ToString();
                    specialFrames = LoadSprites(tempFramePath);
                    if (specialFrames.Length != 0)
                    {
                        itemDatas[i].specialData.specialFrame_1 = specialFrames[0];
                        itemDatas[i].specialData.specialFrame_2 = specialFrames[1];
                        itemDatas[i].specialData.specialFrame_3 = specialFrames[2];
                    }
                    else
                    {
                        Debug.Log("Temp Frame Path : " + tempFramePath);
                    }
                }
                break;
            case ItemType.Flag:
                icons = LoadSprites(iconPath);
                checks = LoadSprites(checkPath);
                animations = LoadSprites(animationPath);
                backgrounds = LoadSprites(backgroundPath);

                itemDatas = new ItemData[icons.Length];
                for (int i = 0; i < itemDatas.Length; i++)
                {
                    itemDatas[i].type = ItemType.Flag;
                    itemDatas[i].flagData = new FlagItemData();

                    itemDatas[i].flagData.basicData.icon = icons[i];
                    itemDatas[i].flagData.basicData.requirement = 0;
                    itemDatas[i].flagData.basicData.state = ItemState.Unlocked;

                    itemDatas[i].flagData.checkableData.check = checks[0];

                    itemDatas[i].flagData.animatableData.animation = animations[0];
                    itemDatas[i].flagData.animatableData.background = backgrounds[0];
                }
                break;
            default:
                break;
        }

        IsReadyToCreate = true;
    }

    //Generic Sprite Loader
    public static Sprite[] LoadSprites(string path)
    {
        //Load images from resources
        var resources = Resources.LoadAll<Texture2D>(path);
        //initialize mapTexs with cached resources
        Texture2D[] textures = new Texture2D[resources.Length];
        for (int i = 0; i < resources.Length; i++)
        {
            textures[i] = resources[i];
            textures[i].name = resources[i].name;
        }
        //Create a sprite array to give [Map : Image] of [Level : prefab]
        Sprite[] sprites = new Sprite[textures.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = Sprite.Create(textures[i],
                new Rect(0, 0, textures[i].width, textures[i].height),
                Vector2.one * 0.5f);
            sprites[i].name = textures[i].name;
        }

        //Free Memmory by Default
        foreach (var resource in resources)
        {
            Resources.UnloadAsset(resource);
        }

        return sprites;
    }

    public static Sprite LoadSprite(string path)
    {
        var resource = Resources.Load(path) as Texture2D;

        Sprite sprite = Sprite.Create(resource, new Rect(0, 0, resource.width, resource.height), Vector2.one * 0.5f);
        sprite.name = resource.name;

        Resources.UnloadAsset(resource);

        return sprite;
    }

    public void Create()
    {
        RectTransform rect;
        for (int i = 0; i < itemDatas.Length; i++)
        {
            //Create Appereance
            rect = Instantiate(itemPrefab);
            rect.SetParent(container.transform);
            rect.transform.localScale = Vector3.one;
            float pos = i * itemDistance;
            rect.transform.localPosition = new Vector3(pos, 0f, 0f);

            //Create Behaviours
            Item item = rect.GetComponent<Item>();

            switch (type)
            {
                case ItemType.Map:
                    item.Data = itemDatas[i];
                    break;
                case ItemType.Body:
                    item.Data = itemDatas[i];
                    break;
                case ItemType.Extra:
                    item.Data = itemDatas[i];
                    break;
                case ItemType.Special:
                    item.Data = itemDatas[i];
                    break;
                case ItemType.Flag:
                    item.Data = itemDatas[i];
                    break;
                default:
                    throw new System.Exception("Type is wrong");
            }

            item.ApplyIconVisual();

            //Apply Visual Components
            switch (type)
            {
                case ItemType.Map:
                    item.SetAnimationVisual(item.Data.mapData.animatableData.animation);
                    item.SetBackgroundVisual(item.Data.mapData.animatableData.background);
                    break;
                case ItemType.Body:
                    item.SetCheckVisual(item.Data.bodyData.checkableData.check);
                    break;
                case ItemType.Extra:
                    item.SetCheckVisual(item.Data.extraData.checkableData.check);
                    break;
                case ItemType.Special:
                    item.SetCheckVisual(item.Data.specialData.checkableData.check);
                    break;
                case ItemType.Flag:
                    item.SetAnimationVisual(item.Data.flagData.animatableData.animation);
                    item.SetBackgroundVisual(item.Data.flagData.animatableData.background);
                    item.SetCheckVisual(item.Data.flagData.checkableData.check);
                    break;
                default:
                    break;
            }

            //Set Point Manager
            item.GetComponent<PointController>().SetManager(pointManager);

            //Name Item
            string itemNumber;
            switch (type)
            {
                case ItemType.Map:
                    itemNumber = itemDatas[i].mapData.basicData.icon.texture.name;
                    itemNumber = itemNumber.Substring(itemNumber.Length - 3);
                    rect.gameObject.name = itemName + " " + itemNumber;
                    break;
                case ItemType.Body:
                    itemNumber = itemDatas[i].bodyData.basicData.icon.texture.name;
                    itemNumber = itemNumber.Substring(itemNumber.Length - 10);
                    rect.gameObject.name = itemName + " " + itemNumber;
                    break;
                case ItemType.Extra:
                    itemNumber = itemDatas[i].extraData.basicData.icon.texture.name;
                    itemNumber = itemNumber.Substring(itemNumber.Length - 10);
                    rect.gameObject.name = itemName + " " + itemNumber;
                    break;
                case ItemType.Special:
                    itemNumber = itemDatas[i].specialData.basicData.icon.texture.name;
                    itemNumber = itemNumber.Substring(itemNumber.Length - 10);
                    rect.gameObject.name = itemName + " " + itemNumber;
                    break;
                case ItemType.Flag:
                    itemNumber = itemDatas[i].flagData.basicData.icon.texture.name;
                    itemNumber = itemNumber.Substring(itemNumber.Length - 3);
                    rect.gameObject.name = itemName + " " + itemNumber;
                    break;
                default:
                    break;
            }
        }

        IsReadyToCreate = false;
    }
}
