using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IndicatorController))]
public class IndicatorControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        IndicatorController controller = (IndicatorController)target;

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Apply"))
        {
            controller.SetVariables(controller.m_kill, controller.m_coin);
            controller.Apply();
        }
        if (GUILayout.Button("Reset"))
        {
            PlayerData.instance.SetDefault();
            controller.Apply();
        }
        EditorGUILayout.EndHorizontal();
    }
}
