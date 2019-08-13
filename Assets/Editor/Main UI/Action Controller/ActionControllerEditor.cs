using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActionController)), CanEditMultipleObjects]
public class ActionControllerEditor : Editor
{
    private ActionController actionController;

    //ActionType
    private SerializedProperty mActionType;
    //ItemType
    private SerializedProperty mİtemType;
    private SerializedProperty mExtraItemType;

    //Event Managers
    private SerializedProperty mPlayerInput;
    private SerializedProperty mSceneTransactor;

    //UI Scroll Specific
    private SerializedProperty mPlayButton;
    private SerializedProperty mLevelInfoText;
    private SerializedProperty mNewMapInfo;
    private SerializedProperty mReachLevelText;

    //Shop Specific
    private SerializedProperty mUseButton;
    private SerializedProperty mPayLockedButton;
    private SerializedProperty mPayUnlockedButton;
    private SerializedProperty mRequirementLockedText;
    private SerializedProperty mRequirementUnlockedText;

    //Indicator Controller
    private SerializedProperty mİndicatorController;

    //Body Specific Enitites
    private SerializedProperty mFrameVisual1;
    private SerializedProperty mFrameVisual2;
    private SerializedProperty mFrameVisual3;

    //Extra Specific Enitities
    private SerializedProperty mExtraVisual;

    //Flag Specific Entities
    private SerializedProperty mFlagIconVisualController;

    //Special Specific Entities
    private SerializedProperty mSpecialFrameVisual1;
    private SerializedProperty mSpecialFrameVisual2;
    private SerializedProperty mSpecialFrameVisual3;

    private void OnEnable()
    {
        actionController = (ActionController)target;

        mActionType = serializedObject.FindProperty("actionType");

        mİtemType = serializedObject.FindProperty("itemType");
        mExtraItemType = serializedObject.FindProperty("extraItemType");

        mPlayerInput = serializedObject.FindProperty("playerInput");
        mSceneTransactor = serializedObject.FindProperty("sceneTransactor");

        mPlayButton = serializedObject.FindProperty("playButton");
        mLevelInfoText = serializedObject.FindProperty("levelInfoText");
        mNewMapInfo = serializedObject.FindProperty("newMapInfo");
        mReachLevelText = serializedObject.FindProperty("reachLevelText");

        mUseButton = serializedObject.FindProperty("useButton");
        mPayLockedButton = serializedObject.FindProperty("payLockedButton");
        mPayUnlockedButton = serializedObject.FindProperty("payUnlockedButton");
        mRequirementLockedText = serializedObject.FindProperty("requirementLockedText");
        mRequirementUnlockedText = serializedObject.FindProperty("requirementUnlockedText");

        mİndicatorController = serializedObject.FindProperty("indicatorController");

        mFrameVisual1 = serializedObject.FindProperty("frameVisual_1");
        mFrameVisual2 = serializedObject.FindProperty("frameVisual_2");
        mFrameVisual3 = serializedObject.FindProperty("frameVisual_3");

        mExtraVisual = serializedObject.FindProperty("extraVisual");

        mFlagIconVisualController = serializedObject.FindProperty("flagIconVisualController");

        mSpecialFrameVisual1 = serializedObject.FindProperty("specialFrameVisual_1");
        mSpecialFrameVisual2 = serializedObject.FindProperty("specialFrameVisual_2");
        mSpecialFrameVisual3 = serializedObject.FindProperty("specialFrameVisual_3");
    }

    public override void OnInspectorGUI()
    {
        //Shows Script
        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((ActionController)target), typeof(ActionController), false);
        GUI.enabled = true;

        serializedObject.Update();

        //Editor Header
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Choose Action Type", EditorStyles.boldLabel);
        actionController.actionType = (ActionType)EditorGUILayout.EnumPopup(new GUIContent("Action Type"), actionController.actionType);
        EditorGUILayout.EndVertical();


        //Editor Body
        EditorGUILayout.BeginVertical();
        switch (actionController.actionType)
        {
            case ActionType.UI:
                actionController.itemType = (ItemType)EditorGUILayout.EnumPopup(new GUIContent("Item Type"), actionController.itemType);
                switch (actionController.itemType)
                {
                    case ItemType.Map:
                        EditorGUILayout.BeginVertical();
                        EditorGUILayout.LabelField("Play Button Components", EditorStyles.boldLabel);
                        EditorGUILayout.PropertyField(mPlayButton, new GUIContent("Play Button"));
                        EditorGUILayout.PropertyField(mPlayerInput, new GUIContent("Player Input"));
                        EditorGUILayout.PropertyField(mSceneTransactor, new GUIContent("Scene Transition"));

                        //actionController.playButton = EditorGUILayout.ObjectField()

                        EditorGUILayout.LabelField("Locked Map Text Components", EditorStyles.boldLabel);
                        EditorGUILayout.PropertyField(mLevelInfoText, new GUIContent("Level Info Object"));
                        EditorGUILayout.PropertyField(mNewMapInfo, new GUIContent("New Map Info Object"));
                        EditorGUILayout.PropertyField(mReachLevelText, new GUIContent("Reach Level Text"));
                        EditorGUILayout.EndVertical();
                        break;
                    default:
                        EditorGUILayout.LabelField("Selected ItemType is not supported in UI Action.\n*UI Action only works with Map Type.", EditorStyles.helpBox);
                        break;
                }
                break;
            case ActionType.Shop:
                EditorGUILayout.LabelField("Coin Indicator", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(mİndicatorController, new GUIContent("Indicator Controller"));

                EditorGUILayout.LabelField("Shop Buttons & Components", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(mUseButton, new GUIContent("Use Button"));

                EditorGUILayout.LabelField("Locked Pay & Requirement", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(mPayLockedButton, new GUIContent("Locked Pay Button"));
                EditorGUILayout.PropertyField(mRequirementLockedText, new GUIContent("Locked Requirement Text"));

                EditorGUILayout.LabelField("Unlocked Pay & Requirement", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(mPayUnlockedButton, new GUIContent("Unlocked Pay Button"));
                EditorGUILayout.PropertyField(mRequirementUnlockedText, new GUIContent("Unlocked Requirement Text"));

                EditorGUILayout.LabelField("Item Type Specific Controllers", EditorStyles.boldLabel);
                actionController.itemType = (ItemType)EditorGUILayout.EnumPopup(new GUIContent("Item Type"), actionController.itemType);
                switch (actionController.itemType)
                {
                    case ItemType.Map:
                        EditorGUILayout.LabelField("Map Properties only Editable in UI Action!", EditorStyles.helpBox);
                        break;
                    case ItemType.Body:
                        EditorGUILayout.LabelField("Body Frame Controllers", EditorStyles.boldLabel);
                        EditorGUILayout.PropertyField(mFrameVisual1, new GUIContent("Frame 1"));
                        EditorGUILayout.PropertyField(mFrameVisual2, new GUIContent("Frame 2"));
                        EditorGUILayout.PropertyField(mFrameVisual3, new GUIContent("Frame 3"));
                        break;
                    case ItemType.Extra:
                        EditorGUILayout.LabelField("Extra Visual Controller", EditorStyles.boldLabel);
                        actionController.extraItemType = (ExtraItemType)EditorGUILayout.EnumPopup(new GUIContent("Extra Item Type"), actionController.extraItemType);
                        EditorGUILayout.PropertyField(mExtraVisual, new GUIContent("Extra Controller"));
                        break;
                    case ItemType.Special:
                        EditorGUILayout.LabelField("Warning! Experimental Fields.", EditorStyles.miniLabel);

                        EditorGUILayout.PropertyField(mSpecialFrameVisual1, new GUIContent("Frame 1"));
                        EditorGUILayout.PropertyField(mSpecialFrameVisual2, new GUIContent("Frame 2"));
                        EditorGUILayout.PropertyField(mSpecialFrameVisual3, new GUIContent("Frame 3"));

                        EditorGUILayout.LabelField("SpecialType suppose to work with animation prefabs.\n" +
                            "For development purposes it works with ExtraVisualController.", EditorStyles.helpBox);
                        break;
                    case ItemType.Flag:
                        EditorGUILayout.LabelField("Flag Icon on UI", EditorStyles.boldLabel);
                        EditorGUILayout.PropertyField(mFlagIconVisualController, new GUIContent("Flag Icon Controller"));

                        if (actionController.indicatorController != null)
                        {
                            EditorGUILayout.LabelField("Flag Type does not need Coin Indicator field to work properly.", EditorStyles.helpBox);
                        }
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }

}
