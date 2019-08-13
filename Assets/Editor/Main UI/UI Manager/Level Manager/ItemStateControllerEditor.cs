using UnityEngine;
using UnityEditor;
using ScrollItemData;

[CustomEditor(typeof(ItemStateController))]
public class ItemStateControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ItemStateController controller = (ItemStateController)target;

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Lock Item"))
        {
            controller.SetMapItemState(ItemState.Locked);
        }
        if (GUILayout.Button("UnlockItem"))
        {
            controller.SetMapItemState(ItemState.Unlocked);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Control Items"))
        {
            controller.CheckStatesAgain();
        }
        EditorGUILayout.EndHorizontal();

    }
}
