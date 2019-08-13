using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public PlayerInput input;

    private UIDataBinder binder;

    private void Start()
    {
        binder = GetComponentInParent<UIDataBinder>();

        input.SetDefaultName(binder.GetName());
    }

    public void SaveInput()
    {
        binder.SetName(input.GetPlayerName());
    }
}
