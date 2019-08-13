#pragma warning disable 649

using UnityEngine;
using UnityEngine.UI;
using ScrollItemData;

public enum ActionType {
    UI,
    Shop
}

/// <summary>
/// Every Item has a different Action. ActionController states specific functionalities for chosen ItemType.
/// </summary>
public class ActionController : MonoBehaviour {
    public ActionType actionType;
    public ItemType itemType;
    public ExtraItemType extraItemType;
    public PlayerInputController playerInput;
    public SceneTrans sceneTransactor;

    #region Components
    private UIDataBinder uiBinder;
    private ShopDataBinder shopBinder;
    private ItemScrollManager scrollManager;
    private SnapManager snapManager;
    #endregion

    #region Cache
    private Item[] items;
    private Item centerItem;
    private ItemData itemData;
    private int index;
    private int coinCount;
    #endregion

    #region UI Button & Attributes
    public GameObject playButton;
    public GameObject levelInfoText;
    public GameObject newMapInfo;
    public Text reachLevelText;
    #endregion

    #region ShopUI Button & Attributes
    public GameObject useButton;
    public GameObject payLockedButton;
    public GameObject payUnlockedButton;
    public Text requirementLockedText;
    public Text requirementUnlockedText;
    #endregion

    #region Coin Indicator
    public IndicatorController indicatorController;
    #endregion

    #region Body Frame Visual Controllers
    public FrameVisualController frameVisual_1;
    public FrameVisualController frameVisual_2;
    public FrameVisualController frameVisual_3;
    #endregion

    #region Extra Items Visual Controller
    public ExtraVisualController extraVisual;
    #endregion

    #region Flag Button Visual Controller
    public FlagIconVisualController flagIconVisualController;
    #endregion

    #region Special Extra Visual Controllers
    public ExtraVisualController specialFrameVisual_1;
    public ExtraVisualController specialFrameVisual_2;
    public ExtraVisualController specialFrameVisual_3;
    #endregion

    #region Behaviour Flag
    /// <summary>
    /// Assign "true" for activation of action behaviours.
    /// </summary>
    public bool IsActivated { get; set; } = false;
    private bool isBodyListenersAdded = false;
    private bool isFlagListenersAdded = false;
    private bool isExtraListenersAdded = false;
    private bool isSpecialListenersAdded = false;
    #endregion

    private void Start() {
        uiBinder = GetComponentInParent<UIDataBinder>();

        scrollManager = GetComponent<ItemScrollManager>();
        snapManager = GetComponent<SnapManager>();
        items = scrollManager.GetItems();

        if (actionType == ActionType.UI)
        {
            newMapInfo.SetActive(false);
            if (PlayerData.instance.GetPlayerLevelUp() != 0)
            {
                newMapInfo.SetActive(true);
                newMapInfo.GetComponent<NewMapInfoAnimController>().Animate();
            }
        }

        if(actionType == ActionType.Shop) {
            shopBinder = GetComponentInParent<ShopDataBinder>();
        }
    }

    private void Update() {
        if(IsActivated || actionType == ActionType.UI) {
            HandleItemButton();
        } else {
            isBodyListenersAdded = false;
            isFlagListenersAdded = false;
            isExtraListenersAdded = false;
            isSpecialListenersAdded = false;
        }
    }

    #region UI Specific Button Actions
    /// <summary>
    /// Loads Game Scene and plays SceneTransition animation.
    /// </summary>
    public void PlayGame() {
        playerInput.SaveInput();
        sceneTransactor.CloseSceneTransition("Main UI");
    }
    #endregion

    #region Shop Specific Button Actions
    #region Generic Item Methods
    private void UncheckAllItems() {
        foreach(var item in items) {
            item.GetComponent<CheckAnimationController>().UncheckItem();
        }
    }
    #endregion

    #region Generic Shop Button Methods
    private void ActivateUnlockedPayButton(string requirement) {
        payLockedButton.SetActive(false);
        useButton.SetActive(false);
        payUnlockedButton.SetActive(true);
        requirementLockedText.text = requirement;
        requirementUnlockedText.text = requirement;
    }

    private void ActivateLockedPayButton(string requirement) {
        payUnlockedButton.SetActive(false);
        useButton.SetActive(false);
        payLockedButton.SetActive(true);
        requirementUnlockedText.text = requirement;
        requirementLockedText.text = requirement;
    }

    private void ActivateUseButton() {
        payLockedButton.SetActive(false);
        payUnlockedButton.SetActive(false);
        useButton.SetActive(true);
    }
    #endregion

    #region Flag Specific
    public void UseFlag() {
        UncheckAllItems();

        shopBinder.SetFlagIcon(index);
        flagIconVisualController.SetVisual(itemData.flagData.basicData.icon);
        centerItem.GetComponent<CheckAnimationController>().CheckItem();
    }
    #endregion

    #region Body Specific
    public void UseBody() {
        UncheckAllItems();

        shopBinder.SetBodyIcon(index);

        frameVisual_1.SetVisual(itemData.bodyData.frame_1);
        frameVisual_2.SetVisual(itemData.bodyData.frame_2);
        frameVisual_3.SetVisual(itemData.bodyData.frame_3);

        centerItem.GetComponent<CheckAnimationController>().CheckItem();
    }

    public void PayBody() {
        UseBody();
        shopBinder.AddBodyIndex(index);
        centerItem.SetShopItemState(ShopItemState.Unlocked);

        uiBinder.PayWithCoin(centerItem.Data.bodyData.basicData.requirement);
        indicatorController.Apply();
    }
    #endregion

    #region Front Specific
    public void UseFront() {
        UncheckAllItems();

        shopBinder.SetFrontIcon(index);
        extraVisual.SetVisual(itemData.extraData.extra);

        centerItem.GetComponent<CheckAnimationController>().CheckItem();
    }

    public void PayFront() {
        UseFront();
        shopBinder.AddFrontIndex(index);
        centerItem.SetShopItemState(ShopItemState.Unlocked);

        uiBinder.PayWithCoin(centerItem.Data.extraData.basicData.requirement);
        indicatorController.Apply();

    }
    #endregion

    #region Top Specific
    public void UseTop() {
        UncheckAllItems();

        shopBinder.SetTopIcon(index);
        extraVisual.SetVisual(itemData.extraData.extra);

        centerItem.GetComponent<CheckAnimationController>().CheckItem();
    }

    public void PayTop() {
        UseTop();
        shopBinder.AddTopIndex(index);
        centerItem.SetShopItemState(ShopItemState.Unlocked);

        uiBinder.PayWithCoin(centerItem.Data.extraData.basicData.requirement);
        indicatorController.Apply();
    }
    #endregion

    #region Down Specific
    public void UseDown() {
        UncheckAllItems();

        shopBinder.SetDownIcon(index);
        extraVisual.SetVisual(itemData.extraData.extra);

        centerItem.GetComponent<CheckAnimationController>().CheckItem();
    }

    public void PayDown() {
        UseDown();
        shopBinder.AddDownIndex(index);
        Debug.Log("Center Index : " + index);
        Debug.Log("Current Index : " + shopBinder.GetDownIndex());
        centerItem.SetShopItemState(ShopItemState.Unlocked);

        uiBinder.PayWithCoin(centerItem.Data.extraData.basicData.requirement);
        indicatorController.Apply();
    }
    #endregion

    #region Special Specific
    public void UseSpecial() {
        UncheckAllItems();

        shopBinder.SetSpecialIcon(index);

        specialFrameVisual_1.SetVisual(itemData.specialData.specialFrame_1);
        specialFrameVisual_2.SetVisual(itemData.specialData.specialFrame_2);
        specialFrameVisual_3.SetVisual(itemData.specialData.specialFrame_3);

        centerItem.GetComponent<CheckAnimationController>().CheckItem();
    }

    public void PaySpecial() {
        UseSpecial();
        shopBinder.AddSpecialIndex(index);
        centerItem.SetShopItemState(ShopItemState.Unlocked);

        uiBinder.PayWithCoin(centerItem.Data.specialData.basicData.requirement);
        indicatorController.Apply();
    }
    #endregion

    #region Shop Buttons Behaviour Handler
    public void HandleItemButton() {
        index = snapManager.SnapIndex;
        centerItem = items[index];
        itemData = centerItem.Data;

        if(actionType == ActionType.Shop) {
            coinCount = uiBinder.GetIndicatorsData().coinCount;
        }

        switch(itemData.type) {
            case ItemType.Map: {
                    switch(itemData.mapData.basicData.state) {
                        case ItemState.Locked:
                            playButton.SetActive(false);

                            reachLevelText.text = "reach level " + itemData.mapData.basicData.requirement.ToString();
                            levelInfoText.SetActive(true);
                            break;
                        case ItemState.Unlocked:
                            uiBinder.SetMapIndex(index);

                            levelInfoText.SetActive(false);
                            playButton.SetActive(true);
                            break;
                        default:
                            throw new System.Exception("Map state has a problem.");
                    }
                    break;
                }
            case ItemType.Body: {
                    if(!isBodyListenersAdded && IsActivated) {
                        HandleBodyListeners();
                    }

                    switch(centerItem.Data.bodyData.payableData.state) {
                        case ShopItemState.Locked: {
                                if(coinCount >= centerItem.Data.bodyData.basicData.requirement) {
                                    ActivateUnlockedPayButton(centerItem.Data.bodyData.basicData.requirement.ToString());
                                } else {
                                    ActivateLockedPayButton(centerItem.Data.bodyData.basicData.requirement.ToString());
                                }
                                break;
                            }
                        case ShopItemState.Unlocked: {
                                ActivateUseButton();
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                }
            case ItemType.Extra: {
                    if(!isExtraListenersAdded && IsActivated) {
                        HandleExtraListeners();
                    }

                    switch(centerItem.Data.extraData.payableData.state) {
                        case ShopItemState.Locked:
                            if(coinCount >= centerItem.Data.extraData.basicData.requirement) {
                                ActivateUnlockedPayButton(centerItem.Data.extraData.basicData.requirement.ToString());
                            } else {
                                ActivateLockedPayButton(centerItem.Data.extraData.basicData.requirement.ToString());
                            }
                            break;
                        case ShopItemState.Unlocked: {
                                ActivateUseButton();
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                }
            case ItemType.Special:
                if(!isSpecialListenersAdded && IsActivated) {
                    HandleSpecialListeners();
                }

                switch(centerItem.Data.specialData.payableData.state) {
                    case ShopItemState.Locked:
                        if(coinCount >= centerItem.Data.specialData.basicData.requirement) {
                            ActivateUnlockedPayButton(centerItem.Data.specialData.basicData.requirement.ToString());
                        } else {
                            ActivateLockedPayButton(centerItem.Data.specialData.basicData.requirement.ToString());
                        }
                        break;
                    case ShopItemState.Unlocked:
                        ActivateUseButton();
                        break;
                    default:
                        break;
                }
                break;
            case ItemType.Flag: {
                    if(!isFlagListenersAdded && IsActivated) {
                        HandleFlagListeners();
                    }
                    payUnlockedButton.SetActive(false);
                    payLockedButton.SetActive(false);
                    useButton.SetActive(true);
                    break;
                }
            default:
                throw new System.Exception("Item Type mismatch.");
        }
    }
    #endregion
    #endregion

    #region Shop UI Selected Item Preparation
    public void PrepareCurrentTopCheck() {
        index = shopBinder.GetTopIndex();
        Item top = items[index];
        CheckAnimationController topAnim = top.GetComponent<CheckAnimationController>();
        topAnim.CheckItem();
    }
    public void PrepareCurrentBodyCheck() {
        index = shopBinder.GetBodyIndex();
        Item body = items[index];
        CheckAnimationController bodyAnim = body.GetComponent<CheckAnimationController>();
        bodyAnim.CheckItem();
    }

    #region Flag Preparations
    public void PrepareCurrentFlagCheck() {
        index = shopBinder.GetFlagIcon();
        Item flag = items[index];
        CheckAnimationController flagAnim = flag.GetComponent<CheckAnimationController>();
        flagAnim.CheckItem();
        flagIconVisualController.SetVisual(flag.Data.flagData.basicData.icon);
    }

    public void PrepareCurrentFlagIcon() {
        Item flag = items[shopBinder.GetFlagIcon()];
        flagIconVisualController.SetVisual(flag.Data.flagData.basicData.icon);
    }
    #endregion

    public void PrepareCurrentFrontCheck() {
        index = shopBinder.GetFrontIndex();
        Item front = items[index];
        CheckAnimationController frontAnim = front.GetComponent<CheckAnimationController>();
        frontAnim.CheckItem();
    }

    public void PrepareCurrentDownCheck() {
        index = shopBinder.GetDownIndex();
        Item down = items[index];
        CheckAnimationController downAnim = down.GetComponent<CheckAnimationController>();
        downAnim.CheckItem();
    }

    public void PrepareCurrentSpecialCheck() {
        index = shopBinder.GetSpecialIndex();
        Item special = items[index];
        CheckAnimationController specialAnim = special.GetComponent<CheckAnimationController>();
        specialAnim.CheckItem();
    }
    #endregion

    #region ItemType Specific Button Listener Handlers
    private void EmptyAllShopButtonsListeners() {
        payLockedButton.GetComponent<Button>().onClick.RemoveAllListeners();
        payUnlockedButton.GetComponent<Button>().onClick.RemoveAllListeners();
        useButton.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void HandleBodyListeners() {
        isBodyListenersAdded = true;

        EmptyAllShopButtonsListeners();

        useButton.GetComponent<Button>().onClick.AddListener(UseBody);
        payUnlockedButton.GetComponent<Button>().onClick.AddListener(PayBody);
    }

    private void HandleSpecialListeners() {
        isSpecialListenersAdded = true;

        EmptyAllShopButtonsListeners();

        useButton.GetComponent<Button>().onClick.AddListener(UseSpecial);
        payUnlockedButton.GetComponent<Button>().onClick.AddListener(PaySpecial);
    }

    private void HandleExtraListeners() {
        isExtraListenersAdded = true;

        EmptyAllShopButtonsListeners();

        switch(extraItemType) {
            case ExtraItemType.Top:
                Debug.Log("I am here");
                useButton.GetComponent<Button>().onClick.AddListener(UseTop);
                payUnlockedButton.GetComponent<Button>().onClick.AddListener(PayTop);
                break;
            case ExtraItemType.Front:
                useButton.GetComponent<Button>().onClick.AddListener(UseFront);
                payUnlockedButton.GetComponent<Button>().onClick.AddListener(PayFront);
                break;
            case ExtraItemType.Down:
                useButton.GetComponent<Button>().onClick.AddListener(UseDown);
                payUnlockedButton.GetComponent<Button>().onClick.AddListener(PayDown);
                break;
            default:
                break;
        }
    }

    private void HandleFlagListeners() {
        isFlagListenersAdded = true;

        EmptyAllShopButtonsListeners();

        useButton.GetComponent<Button>().onClick.AddListener(UseFlag);
    }
    #endregion

}
