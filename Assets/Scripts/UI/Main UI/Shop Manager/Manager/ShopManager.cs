using UnityEngine;

/// <summary>
/// Responsible for creating and editing Shop UI elements
/// </summary>
public class ShopManager : MonoBehaviour
{
    [Header("Shop Scrolls")]
    public GameObject topScroll;
    public GameObject bodyScroll;
    public GameObject flagScroll;
    public GameObject frontScroll;
    public GameObject downScroll;
    public GameObject specialScroll;

    [Header("ShopButtonState Controllers")]
    public ShopButtonStateController topButtonStateController;
    public ShopButtonStateController bodyButtonStateController;
    public ShopButtonStateController flagButtonStateController;
    public ShopButtonStateController frontButtonStateController;
    public ShopButtonStateController downButtonStateController;
    public ShopButtonStateController specialButtonStateController;

    [Header("Shop Action Controllers")]
    public ActionController topAction;
    public ActionController bodyActions;
    public ActionController flagActions;
    public ActionController frontActions;
    public ActionController downActions;
    public ActionController specialActions;

    private void Start()
    {
        Invoke("Initialize", 0.5f);
    }

    // Initialize Shop UI Buttons for actions and 
    // holds last selected items in Shop UI
    public void Initialize()
    {
        HandleShopButtonState(ShopType.Body);
        //Open standart button choice of Body Button
        OpenShopScroll(ShopType.Body);

        //hold last slected shop item
        topAction.PrepareCurrentTopCheck();
        bodyActions.PrepareCurrentBodyCheck();
        frontActions.PrepareCurrentFrontCheck();
        flagActions.PrepareCurrentFlagIcon();
        specialActions.PrepareCurrentSpecialCheck();
    }

    #region Shop Button Communicators
    public void OpenTopShop()
    {
        HandleShopButtonState(ShopType.Top);
        OpenShopScroll(ShopType.Top);
    }
    public void OpenBodyShop()
    {
        HandleShopButtonState(ShopType.Body);
        OpenShopScroll(ShopType.Body);
    }

    public void OpenFlagShop()
    {
        HandleShopButtonState(ShopType.Flag);
        OpenShopScroll(ShopType.Flag);
    }

    public void OpenFrontShop()
    {
        HandleShopButtonState(ShopType.Front);
        OpenShopScroll(ShopType.Front);
    }

    public void OpenDownShop()
    {
        HandleShopButtonState(ShopType.Down);
        OpenShopScroll(ShopType.Down);
    }

    public void OpenSpecialShop()
    {
        HandleShopButtonState(ShopType.Special);
        OpenShopScroll(ShopType.Special);
    }

    public void OpenOnConstructionSite()
    {
        bodyButtonStateController.SetState(ShopButtonState.Idle);
        frontButtonStateController.SetState(ShopButtonState.Idle);
        flagButtonStateController.SetState(ShopButtonState.Idle);

        bodyScroll.SetActive(false);
        frontScroll.SetActive(false);
        flagScroll.SetActive(false);
    }

    public Animator Construction;
    public void PlayConstruction() { Construction.SetBool("IsOpen", true); }
    public void StopConstruction() { Construction.SetBool("IsOpen", false); }


    private void OpenShopScroll(ShopType type)
    {
        topAction.IsActivated = false;
        bodyActions.IsActivated = false;
        flagActions.IsActivated = false;

        frontActions.IsActivated = false;
        downActions.IsActivated = false;
        specialActions.IsActivated = false;

        topScroll.SetActive(false);
        bodyScroll.SetActive(false);
        flagScroll.SetActive(false);

        frontScroll.SetActive(false);
        downScroll.SetActive(false);
        specialScroll.SetActive(false);

        switch (type)
        {
            case ShopType.Body:
                bodyScroll.SetActive(true);

                bodyActions.PrepareCurrentBodyCheck();
                bodyActions.IsActivated = true;
                break;
            case ShopType.Top:
                topScroll.SetActive(true);

                topAction.PrepareCurrentTopCheck();
                topAction.IsActivated = true;
                break;
            case ShopType.Front:
                frontScroll.SetActive(true);

                frontActions.PrepareCurrentFrontCheck();
                frontActions.IsActivated = true;
                break;
            case ShopType.Down:
                downScroll.SetActive(true);

                downActions.PrepareCurrentDownCheck();
                downActions.IsActivated = true;
                break;
            case ShopType.Special:
                specialScroll.SetActive(true);

                specialActions.PrepareCurrentSpecialCheck();
                specialActions.IsActivated = true;
                break;
            case ShopType.Flag:
                flagScroll.SetActive(true);

                flagActions.PrepareCurrentFlagCheck();
                flagActions.IsActivated = true;

                break;
            default:
                throw new System.Exception("Type is inconvenient");
        }
    }

    private void HandleShopButtonState(ShopType type)
    {
        topButtonStateController.SetState(ShopButtonState.Idle);
        bodyButtonStateController.SetState(ShopButtonState.Idle);
        flagButtonStateController.SetState(ShopButtonState.Idle);

        frontButtonStateController.SetState(ShopButtonState.Idle);
        downButtonStateController.SetState(ShopButtonState.Idle);
        specialButtonStateController.SetState(ShopButtonState.Idle);

        switch (type)
        {
            case ShopType.Body:
                bodyButtonStateController.SetState(ShopButtonState.OnClick);
                break;
            case ShopType.Top:
                topButtonStateController.SetState(ShopButtonState.OnClick);
                break;
            case ShopType.Front:
                frontButtonStateController.SetState(ShopButtonState.OnClick);
                break;
            case ShopType.Down:
                downButtonStateController.SetState(ShopButtonState.OnClick);
                break;
            case ShopType.Special:
                specialButtonStateController.SetState(ShopButtonState.OnClick);
                break;
            case ShopType.Flag:
                flagButtonStateController.SetState(ShopButtonState.OnClick);
                break;
            default:
                break;
        }
    }
    #endregion
}
