#pragma warning disable 649

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VibrationOption : MonoBehaviour, IOptions {
    [Header("Visuals")]
    [SerializeField] private Image visual;
    [SerializeField] private Sprite enabledSprite;
    [SerializeField] private Sprite disabledSprite;

    [Header("State")]
    [SerializeField] private OptionState state = OptionState.Enabled;

    private void Start() {
        if(PlayerPrefsManager.GetVibration() == "vibrationOn") {
            state = OptionState.Enabled;
            ApplyVisual(enabledSprite);
        } else {
            state = OptionState.Disabled;
            ApplyVisual(disabledSprite);
        }
    }

    public void ApplyVisual(Sprite sprite) {
        visual.sprite = sprite;
    }

    public void SetState(OptionState state) {
        this.state = state;
    }

    public void SettingsAction() {
        switch(state) {
            case OptionState.Enabled:
                SetState(OptionState.Disabled);
                ApplyVisual(disabledSprite);
                DisableAction();
                break;
            case OptionState.Disabled:
                SetState(OptionState.Enabled);
                ApplyVisual(enabledSprite);
                EnableAction();
                break;
            default:
                throw new System.Exception("Drawer state is unknown!");
        }
    }

    private void EnableAction() {
        Taptic.tapticOn = true;
        Taptic.Selection();
        Taptic.Heavy();
        PlayerPrefsManager.SetVibration("vibrationOn");
    }

    private void DisableAction() {
        //Nothing :(
        Taptic.tapticOn = false;
        Taptic.Selection();

        PlayerPrefsManager.SetVibration("vibrationOff");
    }
}