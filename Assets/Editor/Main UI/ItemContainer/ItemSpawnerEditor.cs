using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemSpawner))]
[CanEditMultipleObjects]
public class ItemSpawnerEditor : Editor
{
    ItemSpawner spawner;

    //Type
    SerializedProperty m_type;
    SerializedProperty m_extraType;
    //Operands
    SerializedProperty m_container;
    SerializedProperty m_itemPrefab;
    
    //Basic Data
    SerializedProperty m_iconPath;
    SerializedProperty m_trimmingPath;
    //Animatable Data
    SerializedProperty m_animationPath;
    SerializedProperty m_backgroundPath;
    //Checkable Data
    SerializedProperty m_checkPath;
    //Map Specific Data
    SerializedProperty m_lockedPath;
    //Body Specific Data
    SerializedProperty m_framePath;
    //Extra Specific Data
    SerializedProperty m_extraPath;
    //Special Specific Data
    SerializedProperty m_specialPath;

    //ScrollView
    SerializedProperty m_itemName;
    SerializedProperty m_PointManager;
    SerializedProperty m_itemDistance;

    private void OnEnable()
    {
        spawner = (ItemSpawner)target;

        m_type = serializedObject.FindProperty("type");
        m_extraType = serializedObject.FindProperty("extraType");

        m_container = serializedObject.FindProperty("container");
        m_itemPrefab = serializedObject.FindProperty("itemPrefab");

        m_iconPath = serializedObject.FindProperty("iconPath");
        m_trimmingPath = serializedObject.FindProperty("trimmingPath");

        m_animationPath = serializedObject.FindProperty("animationPath");
        m_backgroundPath = serializedObject.FindProperty("backgroundPath");

        m_checkPath = serializedObject.FindProperty("checkPath");

        m_lockedPath = serializedObject.FindProperty("lockedPath");

        m_framePath = serializedObject.FindProperty("framePath");

        m_extraPath = serializedObject.FindProperty("extraPath");

        m_specialPath = serializedObject.FindProperty("specialPath");

        m_itemName = serializedObject.FindProperty("itemName");
        m_PointManager = serializedObject.FindProperty("pointManager");
        m_itemDistance = serializedObject.FindProperty("itemDistance");
    }

    public override void OnInspectorGUI()
    {
        //Shows Script
        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((ItemSpawner)target), typeof(ItemSpawner), false);
        GUI.enabled = true;

        //Editor Header
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("ScrollView Properties", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(m_container, new GUIContent("Container"));
        EditorGUILayout.PropertyField(m_itemPrefab, new GUIContent("Prefab"));

        EditorGUILayout.PropertyField(m_type, new GUIContent("Item Type"));
        EditorGUILayout.EndVertical();

        //Editor Dynamic Body
        EditorGUILayout.BeginVertical();
        switch (spawner.type)
        {
            case ItemType.Map:
                EditorGUILayout.LabelField("Basic Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_iconPath, new GUIContent("Icon Path"));
                EditorGUILayout.PropertyField(m_trimmingPath, new GUIContent("How To Trim"));

                EditorGUILayout.LabelField("Animatable Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_animationPath, new GUIContent("Animation Path"));
                EditorGUILayout.PropertyField(m_backgroundPath, new GUIContent("Background Path"));

                EditorGUILayout.LabelField("Map Specific Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_lockedPath, new GUIContent("Locked Path"));
                break;
            case ItemType.Body:
                EditorGUILayout.LabelField("Basic Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_iconPath, new GUIContent("Icon Path"));
                EditorGUILayout.PropertyField(m_trimmingPath, new GUIContent("Requirement Trimmer"));

                EditorGUILayout.LabelField("Checkable Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_checkPath, new GUIContent("Check Path"));

                EditorGUILayout.LabelField("Body Specific Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_framePath, new GUIContent("Frame Path Trimmer"));
                break;
            case ItemType.Extra:
                EditorGUILayout.LabelField("Select Extra Type", EditorStyles.boldLabel);
                spawner.extraType = (ExtraItemType)EditorGUILayout.EnumPopup(new GUIContent("ExtraType"), spawner.extraType);

                EditorGUILayout.LabelField("Basic Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_iconPath, new GUIContent("Icon Path"));
                EditorGUILayout.PropertyField(m_trimmingPath, new GUIContent("How To Trim"));

                EditorGUILayout.LabelField("Checkable Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_checkPath, new GUIContent("Check Path"));

                EditorGUILayout.LabelField("Extra Specific Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_extraPath, new GUIContent("Extra Path"));
                break;
            case ItemType.Special:
                EditorGUILayout.LabelField("Basic Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_iconPath, new GUIContent("Icon Path"));
                EditorGUILayout.PropertyField(m_trimmingPath, new GUIContent("How To Trim"));

                EditorGUILayout.LabelField("Checkable Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_checkPath, new GUIContent("Check Path"));

                EditorGUILayout.LabelField("Special Specific Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_specialPath, new GUIContent("Special Path"));
                break;
            case ItemType.Flag:
                EditorGUILayout.LabelField("Basic Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_iconPath, new GUIContent("Icon Path"));
                EditorGUILayout.PropertyField(m_trimmingPath, new GUIContent("How To Trim"));

                EditorGUILayout.LabelField("Animatable Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_animationPath, new GUIContent("Animation Path"));
                EditorGUILayout.PropertyField(m_backgroundPath, new GUIContent("Background Path"));

                EditorGUILayout.LabelField("Checkable Data Paths", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_checkPath, new GUIContent("Check Path"));
                break;
            default:
                break;
        }
        EditorGUILayout.EndVertical();

        //Editor Footer
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(m_itemName, new GUIContent("Name"));
        EditorGUILayout.PropertyField(m_itemDistance, new GUIContent("Distance"));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.PropertyField(m_PointManager, new GUIContent("Point Manager"));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Empty Container"))
        {
            spawner.EmptyContainer();
        }
        if (GUILayout.Button("Load Resources"))
        {
            spawner.LoadItem();
        }
        EditorGUILayout.EndHorizontal();
        if (spawner.IsReadyToCreate)
        {
            if (GUILayout.Button("Create Items"))
            {
                spawner.Create();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}