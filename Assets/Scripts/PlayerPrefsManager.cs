using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {
    public static void SetVibration(string vibrationState) {
        PlayerPrefs.SetString("vibrationState", vibrationState);
    }
    public static string GetVibration() {
        if(PlayerPrefs.GetString("vibrationState") == "") {
            PlayerPrefs.SetString("vibrationState", "vibrationOn");
        }
        return PlayerPrefs.GetString("vibrationState");
    }
}


