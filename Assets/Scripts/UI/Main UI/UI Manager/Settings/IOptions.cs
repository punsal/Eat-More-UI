using UnityEngine;

public interface IOptions
{
    void ApplyVisual(Sprite visual);
    void SetState(OptionState state);
    void SettingsAction();
}
