using UnityEngine;

[RequireComponent(typeof(ShopButtonStateController))]
public abstract class ShopButtonBehaviourController : MonoBehaviour
{
    protected ShopButtonStateController stateController;

    private void Awake()
    {
        stateController = GetComponent<ShopButtonStateController>();
    }

    protected virtual void ButtonBehaviour()
    {
        switch (stateController.State)
        {
            case ShopButtonState.Idle:
                IdleAction();
                break;
            case ShopButtonState.OnClick:
                OnClickAction();
                break;
            default:
                break;
        }
    }

    public abstract void OnClickAction();
    public abstract void IdleAction();
}
