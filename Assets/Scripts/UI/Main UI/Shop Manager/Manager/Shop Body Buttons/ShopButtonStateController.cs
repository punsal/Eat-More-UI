using UnityEngine;

public class ShopButtonStateController : MonoBehaviour
{
    public ShopButtonState State { get; private set; } = ShopButtonState.Idle;

    public void SetState(ShopButtonState state)
    {
        State = state;
    }
}
