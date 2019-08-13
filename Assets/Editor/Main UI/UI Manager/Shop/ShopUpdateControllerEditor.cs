using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShopUpdateController))]
public class ShopUpdateControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ShopUpdateController controller = (ShopUpdateController)target;

        switch (controller.alertState)
        {
            case AlertState.Enabled:
                if(GUILayout.Button("Stop Alert"))
                {
                    controller.StopAlert();
                }
                break;
            case AlertState.Disabled:
                if(GUILayout.Button("Start Alert"))
                {
                    controller.StartAlert();
                }
                break;
            default:
                throw new System.Exception("Something wrong with the alert state");
        }
    }
}
