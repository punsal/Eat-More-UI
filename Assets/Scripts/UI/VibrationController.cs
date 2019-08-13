using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationController : MonoBehaviour {

    public static void VibrationCheck(Image vibrationButtonImage, Sprite vibrationOnSprite, Sprite vibrationOffSprite) {
        if(PlayerPrefsManager.GetVibration() == "vibrationOn") {
         
            vibrationButtonImage.sprite = vibrationOnSprite;
        } else {
          
            vibrationButtonImage.sprite = vibrationOffSprite;
        }
    }
    public static void ChangeVibrationSelection(Image vibrationButtonImage, Sprite vibrationOnSprite, Sprite vibrationOffSprite) {
        Taptic.tapticOn = !Taptic.tapticOn;
        Taptic.Selection();

        if(Taptic.tapticOn) {
            PlayerPrefsManager.SetVibration("vibrationOn");
            Taptic.Heavy();
            vibrationButtonImage.sprite = vibrationOnSprite;
        } else {
            PlayerPrefsManager.SetVibration("vibrationOff");
            vibrationButtonImage.sprite = vibrationOffSprite;
        }
    }
}
