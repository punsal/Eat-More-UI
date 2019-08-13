using UnityEngine;

public enum AlertState { Enabled, Disabled }

public class ShopUpdateController : MonoBehaviour
{
    [Header("Alert")]
    public ShopAlertController alertController;
    [HideInInspector]
    public AlertState alertState = AlertState.Disabled;

    public void StartAlert()
    {
        alertState = AlertState.Enabled;
        alertController.PlayAnim();
    }

    public void StopAlert()
    {
        alertState = AlertState.Disabled;
        alertController.StopAnim();
    }
}
