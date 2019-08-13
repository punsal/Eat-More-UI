using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelBarController))]
public class LevelBarControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelBarController controller = (LevelBarController)target;
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Apply"))
        {
            controller.SetVariable(controller.m_experience);
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
